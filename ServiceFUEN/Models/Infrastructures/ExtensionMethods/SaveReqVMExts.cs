using System;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;

namespace ServiceFUEN.Models.Infrastructures.ExtensionMethods
{
	
    public static partial class SaveReqVMExts
    {
        public static ActivityCollection ToActivityCollectionEntity(this SaveReqDTO source)
        {
            return new ActivityCollection
            {
                UserId = source.MemberId,
                ActivityId=source.ActivityId

            };
        }
    }

}

