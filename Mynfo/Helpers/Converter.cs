namespace Mynfo.Helpers
{
    using System;
    using Domain;
    using Models;

    public static class Converter
    {
        public static UserLocal ToUserLocal(User user)
        {
            return new UserLocal
            {
                Email = user.Email,
                FirstName = user.FirstName,
                ImagePath = user.ImagePath,
                LastName = user.LastName,
                UserId = user.UserId,
                UserTypeId = user.UserTypeId,
            };
        }

        public static User ToUserDomain(UserLocal user, byte[] imageArray)
        {
            return new User
            {
                Email = user.Email,
                FirstName = user.FirstName,
                ImagePath = user.ImagePath,
                LastName = user.LastName,
                UserId = user.UserId,
                UserTypeId = user.UserTypeId,
                ImageArray = imageArray,
            };
        }

        public static ProfileLocal ToProfileLocalE(ProfileEmail profile)
        {
            return new ProfileLocal
            {
                ProfileName = profile.Name,
                value = profile.Email,
                Logo = "mail2"
            };
        }
        public static ProfileLocal ToProfileLocalE1(ProfileEmail profile)
        {
            return new ProfileLocal
            {
                ProfileName = profile.Name,
                value = profile.Email,
                Logo = "mail3"
            };
        }

        public static ProfileLocal ToProfileLocalP(ProfilePhone profile)
        {
            return new ProfileLocal
            {
                ProfileName = profile.Name,
                value = profile.Number,
                Logo = "tel2"
            };
        }

        public static ProfileLocal ToProfileLocalP1(ProfilePhone profile)
        {
            return new ProfileLocal
            {
                ProfileName = profile.Name,
                value = profile.Number,
                Logo = "tel3"
            };
        }

        public static ProfileLocal ToProfileLocalSM(ProfileSM profile)
        {
            string LogoSM = null;
            switch(profile.RedSocialId)
            {
                case 1:
                    LogoSM = "facebook2";
                    break;
                case 2:
                    LogoSM = "instagramlogo2";
                    break;
                case 3:
                    LogoSM = "twitterlogo2";
                    break;
                case 4:
                    LogoSM = "snapchat2";
                    break;
                case 5:
                    LogoSM = "linkedin2";
                    break;
                case 6:
                    LogoSM = "tiktok2";
                    break;
                case 7:
                    LogoSM = "youtube2";
                    break;
                case 8:
                    LogoSM = "spotify2";
                    break;
                case 9:
                    LogoSM = "twitch2";
                    break;
                case 10:
                    LogoSM = "gmail2";
                    break;
                default:
                    break;
            }

            return new ProfileLocal
            {
                ProfileName = profile.ProfileName,
                value = profile.link,
                RedSocialId = profile.RedSocialId,
                Logo = LogoSM,
            };
        }

        public static ProfileLocal ToProfileLocalSM1(ProfileSM profile)
        {
            string LogoSM = null;
            switch (profile.RedSocialId)
            {
                case 1:
                    LogoSM = "facebook3";
                    break;
                case 2:
                    LogoSM = "instagram3";
                    break;
                case 3:
                    LogoSM = "twitter3";
                    break;
                case 4:
                    LogoSM = "snapchat3";
                    break;
                case 5:
                    LogoSM = "linkedin3";
                    break;
                case 6:
                    LogoSM = "tiktok3";
                    break;
                case 7:
                    LogoSM = "youtube3";
                    break;
                case 8:
                    LogoSM = "spotify3";
                    break;
                case 9:
                    LogoSM = "twitch3";
                    break;
                case 10:
                    LogoSM = "gmail3";
                    break;
                default:
                    break;
            }

            return new ProfileLocal
            {
                ProfileName = profile.ProfileName,
                value = profile.link,
                RedSocialId = profile.RedSocialId,
                Logo = LogoSM,
            };
        }

        public static ProfileLocal ToProfileLocalW(ProfileWhatsapp profile)
        {
            return new ProfileLocal
            {
                ProfileName = profile.Name,
                value = profile.Number,
                Logo = "whatsapp2"
            };
        }

        public static ProfileLocal ToProfileLocalW1(ProfileWhatsapp profile)
        {
            return new ProfileLocal
            {
                ProfileName = profile.Name,
                value = profile.Number,
                Logo = "whatsapp3"
            };
        }

    }
}
