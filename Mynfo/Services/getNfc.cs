using Mynfo.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Mynfo.Services
{
    public class getNfc
    {
        public static string get_box()
        {
            string json = null;
            string message2 = null;
            try
            {
                json = null;
                var Profile = new ProfileLocal();
                var Profile_1 = new ProfileLocal();
                var Box_Local = new BoxLocal();
                using (var conn = new SQLite.SQLiteConnection(App.root_db))
                {
                    Profile_1 = conn.Table<ProfileLocal>().FirstOrDefault();
                    Box_Local = conn.Table<BoxLocal>().FirstOrDefault();
                    int coun = conn.Table<ProfileLocal>().Count();
                    string json_header = null;
                    string json_body = null;
                    string json_value = null;
                    string json_fantasma = null;                    

                    //11/30/2020 6:09:07 p. m.

                    json_header = "Box recibida correctamente!\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n" +
                           "¡";

                    json_value = "{"
                              + @"""BoxId"":""" + Box_Local.BoxId + @""",
                                ""Name"":""" + Box_Local.Name + @""",   
                                ""BoxDefault"":""" + Box_Local.BoxDefault + @""",
                                ""UserId"":""" + Box_Local.UserId + @""",
                                ""Time"":""" + Box_Local.Time + @""",
                                ""ImagePath"":""" + Box_Local.ImagePath + @""",
                                ""UserTypeId"":""" + Box_Local.UserTypeId + @""",
                                ""FirstName"":""" + Box_Local.FirstName + @""",
                                ""LastName"":""" + Box_Local.LastName + @""",
                                ""ImageFullPath"":""" + Box_Local.ImageFullPath + @""",
                                ""FullName"":""" + Box_Local.FullName + @""",
                                ""ProfileLocalId"":""" + Profile_1.ProfileLocalId + @""",
                                ""IdBox"":""" + Profile_1.IdBox + @""",
                                ""UserId_p"":""" + Profile_1.UserId + @""",
                                ""ProfileName"":""" + Profile_1.ProfileName + @""",
                                ""value"":""" + Profile_1.value + @""",
                                ""ProfileType"":""" + Profile_1.ProfileType + @"""                                                              
                                }";


                    message2 = Box_Local.UserId.ToString(); 


                    //quitar
                    //name bxl
                    //BoxDefault
                    //time
                    //UserTypeId 
                    //ImageFullPath
                    //FullName


                    json_fantasma = "{"
                              + @"""BoxId"":""-"",
                                ""Name"":""" + Box_Local.Name + @""",
                                ""BoxDefault"":""" + Box_Local.BoxDefault + @""",
                                ""UserId"":""" + Box_Local.UserId + @""",
                                ""Time"":""" + Box_Local.Time + @""",
                                ""ImagePath"":""" + Box_Local.ImagePath + @""",
                                ""UserTypeId"":""" + Box_Local.UserTypeId + @""",
                                ""FirstName"":""" + Box_Local.FirstName + @""",
                                ""LastName"":""" + Box_Local.LastName + @""",
                                ""ImageFullPath"":""" + Box_Local.ImageFullPath + @""",
                                ""FullName"":""" + Box_Local.FullName + @""",
                                ""ProfileLocalId"":""" + Profile_1.ProfileLocalId + @""",
                                ""IdBox"":""" + Profile_1.IdBox + @""",
                                ""UserId_p"":""" + Profile_1.UserId + @""",
                                ""ProfileName"":""" + Profile_1.ProfileName + @""",
                                ""value"":""" + Profile_1.value + @""",
                                ""ProfileType"":""" + Profile_1.ProfileType + @"""                                                              
                                }";

                    if (coun > 1)
                    {
                        for (int i = 1; i < coun; i++)
                        {
                            Profile = conn.Table<ProfileLocal>().ElementAt(i);

                            json_body = "{"
                              + @"""BoxId"":""" + Box_Local.BoxId + @""",
                                ""Name"":""" + Box_Local.Name + @""",
                                ""BoxDefault"":""" + Box_Local.BoxDefault + @""",
                                ""UserId"":""" + Box_Local.UserId + @""",
                                ""Time"":""" + Box_Local.Time + @""",
                                ""ImagePath"":""" + Box_Local.ImagePath + @""",
                                ""UserTypeId"":""" + Box_Local.UserTypeId + @""",
                                ""FirstName"":""" + Box_Local.FirstName + @""",
                                ""LastName"":""" + Box_Local.LastName + @""",
                                ""ImageFullPath"":""" + Box_Local.ImageFullPath + @""",
                                ""FullName"":""" + Box_Local.FullName + @""",
                                ""ProfileLocalId"":""" + Profile.ProfileLocalId + @""",
                                ""IdBox"":""" + Profile.IdBox + @""",
                                ""UserId_p"":""" + Profile.UserId + @""",
                                ""ProfileName"":""" + Profile.ProfileName + @""",
                                ""value"":""" + Profile.value + @""",
                                ""ProfileType"":""" + Profile.ProfileType + @"""                                                              
                                }";

                            json_value = json_value + ",\n" + json_body;
                        }
                        json_value = "[" + json_value + "]";
                    }
                    else
                    {
                        json_value = "[" + json_value + ",\n" + json_fantasma + "]";
                    }


                    json = "¬" + json_value;
                }
            }
            catch (Exception exx)
            {
                Console.Write(exx);
                json = null;
            }
            var message = json;
            return message2;
        }
    }
}