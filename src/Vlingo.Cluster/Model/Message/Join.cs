// Copyright © 2012-2018 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Cluster.Model.Message
{
    using Vlingo.Wire.Node;
    
    public sealed class Join : OperationalMessage
    {
        private readonly Node _node;
        
        public static Join From(string content) => new Join(OperationalMessagePartsBuilder.NodeFrom(content));
        
        public Join(Node node) : base(node.Id)
        {
            _node = node;
        }

        public override bool IsJoin => true;
        
        public Node Node => _node;

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Join))
            {
                return false;
            }

            var join = (Join) obj;

            return Node.Equals(join.Node);
        }

        public override int GetHashCode() => 31 * _node.GetHashCode();

        public override string ToString() => $"Join[{_node}]";
    }
}