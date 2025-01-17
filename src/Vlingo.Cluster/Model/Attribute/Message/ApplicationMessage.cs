// Copyright © 2012-2018 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Cluster.Model.Attribute.Message
{
    using Vlingo.Wire.Node;
    
    public abstract class ApplicationMessage
    {
        public const string NoCorrelatingMessageId = "-";
        
        public static string TrackingIdFor(Node node, ApplicationMessageType type, string trailingId) =>
            $"{node.Name.Value}:{type.ToString()}:{trailingId}";
        
        public abstract string ToPayload();

        public override string ToString() => ToPayload();

        public string CorrelatingMessageId { get; }
        
        public string TrackingId { get; }
        
        public ApplicationMessageType Type { get; }
        
        protected ApplicationMessage(string correlatingMessageId, ApplicationMessageType type, string trackingId)
        {
            CorrelatingMessageId = correlatingMessageId;
            TrackingId = trackingId;
            Type = type;
        }
    }
}