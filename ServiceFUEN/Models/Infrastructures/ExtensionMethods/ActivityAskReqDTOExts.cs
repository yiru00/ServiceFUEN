using System;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;

namespace ServiceFUEN.Models.Infrastructures.ExtensionMethods
{
	
    public static partial class ActivityAskReqDTOExts
    {
        public static Question ToQuestionEntity(this ActivityAskReqDTO source)
        {
            return new Question
            {
                MemberId=source.MemberId,
                ActivityId=source.ActivityId,
                Content=source.content

            };
        }
    }

}

