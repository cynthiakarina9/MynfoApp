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
            };
        }

        public static ProfileLocal ToProfileLocalP(ProfilePhone profile)
        {
            return new ProfileLocal
            {
                ProfileName = profile.Name,
                value = profile.Number,
            };
        }

        public static ProfileLocal ToProfileLocalSM(ProfileSM profile)
        {
            return new ProfileLocal
            {
                ProfileName = profile.ProfileName,
                value = profile.link,
            };
        }

        public static ProfileLocal ToProfileLocalW(ProfileWhatsapp profile)
        {
            return new ProfileLocal
            {
                ProfileName = profile.Name,
                value = profile.Number,
            };
        }

    }
}
