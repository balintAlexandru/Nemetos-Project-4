using Back_End.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Back_End.DTO

{
    public class SocialMediaDTO
    {
        public int IdSocialMedia { get; set; }
        public string Facebook { get; set; }
        public string Google { get; set; }
        public string Twitter { get; set; }
        public string Linkedin { get; set; }
        public int EmployeesRef { get; set; }
        [JsonIgnore]
        public EmployeeDTO Employees { get; set; }

        public static SocialMedia mappingDTOtoEntity(SocialMediaDTO socialMediaDTO)
        {
            SocialMedia socialMediaEntity = new SocialMedia()
            {
                IdSocialMedia = socialMediaDTO.IdSocialMedia,
                Facebook = socialMediaDTO.Facebook,
                Google = socialMediaDTO.Google,
                Twitter = socialMediaDTO.Twitter,
                Linkedin = socialMediaDTO.Linkedin,
                EmployeesRef = socialMediaDTO.EmployeesRef,
               
            };    

            return socialMediaEntity;
        }

    }



}
