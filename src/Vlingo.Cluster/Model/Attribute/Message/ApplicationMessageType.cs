// Copyright © 2012-2018 Vaughn Vernon. All rights reserved.
//
// This Source Code Form is subject to the terms of the
// Mozilla Public License, v. 2.0. If a copy of the MPL
// was not distributed with this file, You can obtain
// one at https://mozilla.org/MPL/2.0/.

namespace Vlingo.Cluster.Model.Attribute.Message
{
    public enum ApplicationMessageType
    {
        CreateAttributeSet,
        AddAttribute,
        ReplaceAttribute,
        RemoveAttribute,
        RemoveAttributeSet,
        ConfirmCreateAttributeSet,
        ConfirmAddAttribute,
        ConfirmReplaceAttribute,
        ConfirmRemoveAttribute,
        ConfirmRemoveAttributeSet
    }
}