// Copyright © 2012-2018 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System.Collections.Generic;

namespace Vlingo.Cluster.Model.Node
{
    using Vlingo.Wire.Node;
    public interface IRegistryInterest
    {
        void InformAllLiveNodes(IEnumerable<Node> liveNodes,  bool isHealthyCluster);
        
        void InformConfirmedByLeader(Node node,  bool isHealthyCluster);
        
        void InformCurrentLeader(Node node,  bool isHealthyCluster);
        
        void InformMergedAllDirectoryEntries(IEnumerable<Node> liveNodes,  IEnumerable<MergeResult> mergeResults,  bool isHealthyCluster);
        
        void InformLeaderDemoted(Node node,  bool isHealthyCluster);
        
        void InformNodeIsHealthy(Node node,  bool isHealthyCluster);
        
        void InformNodeJoinedCluster(Node node,  bool isHealthyCluster);
        
        void InformNodeLeftCluster(Node node,  bool isHealthyCluster);
        
        void InformNodeTimedOut(Node node,  bool isHealthyCluster);
    }
}