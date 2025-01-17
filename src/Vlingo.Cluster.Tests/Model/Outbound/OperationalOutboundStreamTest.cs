// Copyright © 2012-2018 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;
using Vlingo.Actors;
using Vlingo.Actors.TestKit;
using Vlingo.Cluster.Model.Message;
using Vlingo.Cluster.Model.Outbound;
using Vlingo.Wire.Fdx.Outbound;
using Vlingo.Wire.Message;
using Xunit;
using Xunit.Abstractions;

namespace Vlingo.Cluster.Tests.Model.Outbound
{
    using Vlingo.Wire.Node;
    
    public class OperationalOutboundStreamTest : AbstractClusterTest
    {
        private MockManagedOutboundChannelProvider _channelProvider;
        private Id _localNodeId;
        private Node _localNode;
        private TestActor<IOperationalOutboundStream> _outboundStream;
        private TestWorld _world;

        [Fact]
        public void TestDirectory()
        {
            _outboundStream.Actor.Directory(new HashSet<Node>(Config.AllNodes));

            foreach (var channel in AllTargetChannels())
            {
                var message = OperationalMessage.MessageFrom(Mock(channel).Writes[0]);
                Assert.True(message.IsDirectory);
                Assert.Equal(_localNodeId, message.Id);
            }
        }
        
        [Fact]
        public void TestElect()
        {
            _outboundStream.Actor.Elect(Config.AllGreaterNodes(_localNodeId));

            foreach (var channel in AllTargetChannels())
            {
                var message = OperationalMessage.MessageFrom(Mock(channel).Writes[0]);
                Assert.True(message.IsElect);
                Assert.Equal(_localNodeId, message.Id);
            }
        }
        
        [Fact]
        public void TestJoin()
        {
            _outboundStream.Actor.Join();

            foreach (var channel in AllTargetChannels())
            {
                var message = OperationalMessage.MessageFrom(Mock(channel).Writes[0]);
                Assert.True(message.IsJoin);
                Assert.Equal(_localNodeId, message.Id);
            }
        }
        
        [Fact]
        public void TestLeader()
        {
            _outboundStream.Actor.Leader();

            foreach (var channel in AllTargetChannels())
            {
                var message = OperationalMessage.MessageFrom(Mock(channel).Writes[0]);
                Assert.True(message.IsLeader);
                Assert.Equal(_localNodeId, message.Id);
            }
        }
        
        [Fact]
        public void TestLeaderOfId()
        {
            var targetId = Id.Of(3);
            
            _outboundStream.Actor.Leader(targetId);

            var channel = _channelProvider.ChannelFor(targetId);
            var message = OperationalMessage.MessageFrom(Mock(channel).Writes[0]);
            Assert.True(message.IsLeader);
            Assert.Equal(_localNodeId, message.Id);
        }
        
        [Fact]
        public void TestLeave()
        {
            _outboundStream.Actor.Leave();

            foreach (var channel in AllTargetChannels())
            {
                var message = OperationalMessage.MessageFrom(Mock(channel).Writes[0]);
                Assert.True(message.IsLeave);
                Assert.Equal(_localNodeId, message.Id);
            }
        }
        
        [Fact]
        public void TestPing()
        {
            var targetId = Id.Of(3);
            
            _outboundStream.Actor.Ping(targetId);

            var channel = _channelProvider.ChannelFor(targetId);
            var message = OperationalMessage.MessageFrom(Mock(channel).Writes[0]);
            Assert.True(message.IsPing);
            Assert.Equal(_localNodeId, message.Id);
        }
        
        [Fact]
        public void TestPulseToTarget()
        {
            var targetId = Id.Of(3);
            
            _outboundStream.Actor.Pulse(targetId);

            var channel = _channelProvider.ChannelFor(targetId);
            var message = OperationalMessage.MessageFrom(Mock(channel).Writes[0]);
            Assert.True(message.IsPulse);
            Assert.Equal(_localNodeId, message.Id);
        }
        
        [Fact]
        public void TestPulse()
        {
            _outboundStream.Actor.Pulse();

            foreach (var channel in AllTargetChannels())
            {
                var message = OperationalMessage.MessageFrom(Mock(channel).Writes[0]);
                Assert.True(message.IsPulse);
                Assert.Equal(_localNodeId, message.Id);
            }
        }
        
        [Fact]
        public void TestSplit()
        {
            var targetNodeId = Id.Of(2);
            var currentLeaderId = Id.Of(3);
    
            _outboundStream.Actor.Split(targetNodeId, currentLeaderId);
    
            var channel = _channelProvider.ChannelFor(targetNodeId);
            var message = OperationalMessage.MessageFrom(Mock(channel).Writes[0]);
            Assert.True(message.IsSplit);
            Assert.Equal(currentLeaderId, message.Id);
        }
        
        [Fact]
        public void TestVote()
        {
            var targetNodeId = Id.Of(2);
            
            _outboundStream.Actor.Vote(targetNodeId);

            var channel = _channelProvider.ChannelFor(targetNodeId);
            var message = OperationalMessage.MessageFrom(Mock(channel).Writes[0]);
            Assert.True(message.IsVote);
            Assert.Equal(_localNodeId, message.Id);
        }
        
        public OperationalOutboundStreamTest(ITestOutputHelper output) : base(output)
        {
            _world = TestWorld.Start("test-outbound-stream");
    
            _localNodeId = Id.Of(1);
    
            _localNode = Config.NodeMatching(_localNodeId);
    
            _channelProvider = new MockManagedOutboundChannelProvider(_localNodeId, Config);
    
            var pool = new ByteBufferPool(10, Properties.OperationalBufferSize());
    
            _outboundStream =
                _world.ActorFor<IOperationalOutboundStream>(
                    Definition.Has<OperationalOutboundStreamActor>(Definition.Parameters(_localNode, _channelProvider, pool)));
        }

        public override void Dispose()
        {
            _world.Terminate();
            base.Dispose();
        }
        
        private MockManagedOutboundChannel Mock(IManagedOutboundChannel channel)
        {
            return (MockManagedOutboundChannel) channel;
        }

        private List<IManagedOutboundChannel> AllTargetChannels()
        {
            return new List<IManagedOutboundChannel>(_channelProvider.AllOtherNodeChannels.Values);
        }
    }
}