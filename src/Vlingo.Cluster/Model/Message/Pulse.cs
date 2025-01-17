// Copyright © 2012-2018 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Wire.Node;

namespace Vlingo.Cluster.Model.Message
{
    public sealed class Pulse : OperationalMessage
    {
        public static Pulse From(string content) =>  new Pulse(OperationalMessagePartsBuilder.IdFrom(content));
        
        public Pulse(Id id) : base(id)
        {
        }

        public override bool IsPulse => true;
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(Pulse))
            {
                return false;
            }

            return Id.Equals(((Pulse) obj).Id);
        }

        public override int GetHashCode() => 31 * Id.GetHashCode();

        public override string ToString() => $"Pulse[{Id}]";
    }
}