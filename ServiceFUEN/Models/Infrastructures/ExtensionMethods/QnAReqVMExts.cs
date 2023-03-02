using System;
using ServiceFUEN.Models.DTOs;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;

namespace ServiceFUEN.Models.Infrastructures.ExtensionMethods
{
	
    public static partial class QnAReqVMExts
    {
        public static QnAResVM ToQnAResVM(this Question source)
        {
            if (source.Answers.FirstOrDefault()==null)
            {
                return new QnAResVM()
                {
                    activityId = source.ActivityId,
                    QId = source.Id,
                    QContent = source.Content,
                    QDateCreated = source.DateCreated.ToString("yyyy-MM-dd HH:mm"),
                    MemberId = source.Member.Id,

                    NickName = source.Member.NickName,
                    PhotoSticker = source.Member.PhotoSticker,

                    //一個問題只會有一個答案
                    AId = null,
                    AContent = null,
                    ADateCreated = null
                };
            }
            return new QnAResVM()
            {
                activityId = source.ActivityId,
                QId = source.Id,
                QContent = source.Content,
                QDateCreated = source.DateCreated.ToString("yyyy-MM-dd HH:mm"),
                MemberId = source.Member.Id,

                NickName = source.Member.NickName,
                PhotoSticker = source.Member.PhotoSticker,

                //一個問題只會有一個答案
                AId = source.Answers.FirstOrDefault().Id,
                AContent = source.Answers.FirstOrDefault().Content,
                ADateCreated = source.Answers.FirstOrDefault().DateCreated.ToString("yyyy-MM-dd HH:mm"),
            };
    
        }
    }

}

