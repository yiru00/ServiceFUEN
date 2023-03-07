using Microsoft.AspNetCore.Routing;
using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
    public static partial class ActivityMemberExts
    {
        public static EnrollRecordResVM ToEnrollRecordResVM(this ActivityMember source)
        {
            return new EnrollRecordResVM
            {
                MemberId = source.MemberId,
                ActivityId = source.ActivityId,
                Route ="/Activity/" + source.ActivityId,
                CoverImage = source.Activity.CoverImage,
                ActivityName = source.Activity.ActivityName,
                Recommendation = source.Activity.Recommendation,
                Address = source.Activity.Address,
                MemberLimit = source.Activity.MemberLimit,
                Description = source.Activity.Description,
                GatheringTime = source.Activity.GatheringTime.ToString("yyyy-MM-dd HH:mm"),
                Deadline = source.Activity.Deadline.ToString("yyyy-MM-dd HH:mm"),
                DateOfCreated = source.Activity.DateOfCreated.ToString("yyyy-MM-dd HH:mm"),
                EnrollId = source.Id,
                DateJoined = source.DateJoined.ToString("yyyy-MM-dd HH:mm"),
            };

        }

        

    }
}

