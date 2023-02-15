using System;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;

namespace ServiceFUEN.Models.Infrastructures.ExtensionMethods
{
	
    public static partial class EnrollReqVMExts
    {
        public static ActivityMember ToActivityMemberEntity(this EnrollReqVM source)
        {
            return new ActivityMember
            {
                MemberId = source.MemberId,
                ActivityId=source.ActivityId

            };
        }
    }

}

