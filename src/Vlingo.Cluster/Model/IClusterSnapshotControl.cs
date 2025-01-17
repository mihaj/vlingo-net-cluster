// Copyright © 2012-2018 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using System;
using Vlingo.Actors;
using Vlingo.Cluster.Model.Application;

namespace Vlingo.Cluster.Model
{
    public interface IClusterSnapshotControl
    {
        void ShutDown();
    }

    public class ClusterSnapshotControlFactory
    {
        public static Tuple<IClusterSnapshotControl, ILogger> Instance(World world, string name)
        {
            var initializer = new ClusterSnapshotInitializer(name, Properties.Instance, world.DefaultLogger);
            
            var application = ClusterApplicationFactory.Instance(world, initializer.LocalNode);
            
            var definition =
                    Definition.Has<ClusterSnapshotActor>(
                        Definition.Parameters(initializer, application),
                        "cluster-snapshot-" + name);
            
            var control = world.ActorFor<IClusterSnapshotControl>(definition);
            
            return new Tuple<IClusterSnapshotControl, ILogger>(control, world.DefaultLogger);
        }
    }
}