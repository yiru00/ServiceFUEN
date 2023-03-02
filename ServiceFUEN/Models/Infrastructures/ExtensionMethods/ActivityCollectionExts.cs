using System;
using ServiceFUEN.Models.EFModels;
using ServiceFUEN.Models.ViewModels;

namespace ServiceFUEN.Models.Infrastructures.ExtensionMethods
{
	public static partial class ActivityCollectionExts
	{
        public static SaveRecordResVM ToSaveRecordResVM(this ActivityCollection source)
        {
            return new SaveRecordResVM
            {
                MemberId = source.UserId,
                ActivityId = source.ActivityId,
                CoverImage = source.Activity.CoverImage,
                ActivityName = source.Activity.ActivityName,
                Recommendation = source.Activity.Recommendation,
                Address = source.Activity.Address,
                MemberLimit = source.Activity.MemberLimit,
                Description = source.Activity.Description,
                GatheringTime = source.Activity.GatheringTime.ToString("yyyy-MM-dd HH:mm"),
                Deadline = source.Activity.Deadline.ToString("yyyy-MM-dd HH:mm"),
                DateOfCreated = source.Activity.DateOfCreated.ToString("yyyy-MM-dd HH:mm"),
                SaveId = source.Id,
                DateOfSave = source.DateCreated.ToString("yyyy-MM-dd HH:mm")
            };

        }
    }
}

