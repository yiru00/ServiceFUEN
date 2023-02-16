using ServiceFUEN.Models.EFModels;

namespace ServiceFUEN.Models.ViewModels
{
    public static partial class EnrollRecordResVMExts
    {
        public static EnrollRecordResVM ToEnrollRecordResVM(this ActivityMember source)
        {
            return new EnrollRecordResVM
            {
                MemberId = source.MemberId,
                ActivityId = source.ActivityId,
                CoverImage = source.Activity.CoverImage,
                ActivityName = source.Activity.ActivityName,
                Recommendation = source.Activity.Recommendation,
                Address = source.Activity.Address,
                MemberLimit = source.Activity.MemberLimit,
                Description = source.Activity.Description,
                GatheringTime = source.Activity.GatheringTime,
                Deadline = source.Activity.Deadline,
                DateOfCreated = source.Activity.DateOfCreated,
                EnrollId = source.Id,
                DateJoined = source.DateJoined
            };

        }
        
    }
}

