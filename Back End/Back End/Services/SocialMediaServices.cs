using Back_End.Data;
using Back_End.DTO;

namespace Back_End.Services
{
    public interface ISocialMediaServices
    {
        void addSocialMedia(SocialMediaDTO socialMediaDTO, int id);

    }
    public class SocialMediaServices : ISocialMediaServices
    {
        private EmployeeContext _db { get; set; }

        public SocialMediaServices(EmployeeContext db)
        {
            this._db = db;
        }
        public void addSocialMedia(SocialMediaDTO socialMediaDTO, int id)
        {
            var socialMediaEntity = SocialMediaDTO.mappingDTOtoEntity(socialMediaDTO);
            var employee = _db.Employees.Find(id);
            employee.SocialMedia = socialMediaEntity;
            socialMediaEntity.Employees = employee;

            _db.SocialMedia.Add(socialMediaEntity);
            _db.SaveChanges();

        }
    }
}
