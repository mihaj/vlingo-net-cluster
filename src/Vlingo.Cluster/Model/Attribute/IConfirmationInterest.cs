// Copyright © 2012-2018 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

using Vlingo.Cluster.Model.Attribute.Message;
using Vlingo.Wire.Node;

namespace Vlingo.Cluster.Model.Attribute
{
    public interface IConfirmationInterest
    {
        void Confirm(Id confirmingNodeId, string attributeSetName, string attributeName, ApplicationMessageType type);
    }
}