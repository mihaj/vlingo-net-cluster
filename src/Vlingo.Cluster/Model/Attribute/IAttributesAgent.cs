// Copyright © 2012-2018 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Actors;
using Vlingo.Cluster.Model.Application;
using Vlingo.Cluster.Model.Outbound;
using Vlingo.Common;
using Vlingo.Wire.Fdx.Inbound;

namespace Vlingo.Cluster.Model.Attribute
{
    using Vlingo.Wire.Node;
    
    public interface IAttributesAgent : IAttributesCommands, INodeSynchronizer, IInboundStreamInterest, IScheduled<object>, IStoppable
    {
    }
    
    public static class AttributesAgentFactory
    {
        public static IAttributesAgent Instance(
            Stage stage,
            Node node,
            IClusterApplication application,
            IOperationalOutboundStream outbound,
            IConfiguration configuration)
        {
            var definition =
                    Definition.Has<AttributesAgentActor>(
                Definition.Parameters(node, application, outbound, configuration),
            "attributes-agent");
            
            var attributesAgent = stage.ActorFor<IAttributesAgent>(definition);
            
            return attributesAgent;
        }
    }
}