using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using TestCreator.Objects;

namespace TestCreator.TestCreatorAPI
{
    class Client
    {
        public struct Urls
        {
            public static string users = "/users/";
            public static string usersLogin = "/users/login/";
            public static string usersLoginIsBusy = "/users/loginIsBusy/";
            public static string usersId = "/users/id/";
            public static string usersActivation = "/users/activation/";
            public static string groupByUser = "/users/findAllGroups/";
            public static string userGroupByUser = "/users/findAllUserGroups/";
            public static string usersByGroup = "/users/findAllUserByGroup/";
            public static string groupsInvitationLink = "/users/invitation/";

            public static string findGroupTestByTest = "/groupTests/findGroupTestByTest/";

            public static string testsGetByUser = "/authors/getTests/";

            public static string groups = "/groups/";

            public static string questions = "/questions/";

            public static string tests = "/tests/";

            public static string passedTests = "/passedTests/";

            public static string UserGroups = "/userGroups/";
            public static string UserGroupsGetIdByUserAndGroup = "/userGroups/getIdByUserAndGroup/";



        }

        public struct Method
        {
            public static string POST = "POST";
            public static string GET = "GET";
            public static string DELETE = "DELETE";
            public static string PUT = "PUT";
        }


        private const string port = "8080";
        //private const string host = "192.168.0.147";
        private const string host = "localhost";

        public static string sendRequest(string body, string uri, string method)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = method;

                if (body.Length > 0)
                {
                    using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                    {
                        streamWriter.Write(body);
                        streamWriter.Flush();
                    }
                }


                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    return result;
                }
            }
            catch(Exception ee)
            {
                return ee.ToString()  ;
            }
            

        }

        private static string GenerateUrl(string url)
        {
            return string.Format("http://{0}:{1}{2}", host, port, url);
        }


        //---------------------------------------------USER----------------------------------------------------------
        public static bool signUpUser(User user)
        {
            try
            {
                string json = JsonSerializer.Serialize(user);
                string uri = GenerateUrl(Urls.users);
                return Convert.ToBoolean(sendRequest(json, uri, Method.POST));
            }
            catch(Exception e)
            {

            }
            return false;
        }

        public static User updateUser(User user, long id)
        {
            try
            {
                string json = JsonSerializer.Serialize(user);
                string uri = GenerateUrl(Urls.users + id);
                return JsonSerializer.Deserialize<User>(sendRequest(json, uri, Method.PUT));
            }
            catch (Exception e)
            {

            }
            return user;
           
        }

        public static bool confirmUser(string link)
        {
            return Convert.ToBoolean(sendRequest("", link, Method.GET));
        }

        public static User getUserbyLogin(string login)
        {
            try
            {
                string uri = GenerateUrl(Urls.usersLogin + login);
                User user = JsonSerializer.Deserialize<User>(sendRequest("", uri, Method.GET));
                return user;
            }
            catch(Exception ee)
            {
                return null;
            }
            
        }
        public static string userLoginIsBusy(string login)
        {
            string uri = GenerateUrl(Urls.usersLoginIsBusy + login);
            return sendRequest("", uri, Method.GET);
        }

        public static void deleteUserByLogin(string login)
        {
            string uri = GenerateUrl(Urls.usersLogin + login);
            sendRequest("", uri, Method.DELETE);
        }

        public static ObservableCollection<Group> getGroupsByUser(User user)
        {
            string json = JsonSerializer.Serialize(user);
            string uri = GenerateUrl(Urls.groupByUser);
            string jsonL = sendRequest(json, uri, Method.POST);
            return JsonSerializer.Deserialize<ObservableCollection<Group>>(jsonL);
        }

        


        public static ObservableCollection<User> getuserByGroup(Group group)
        {
            string json = JsonSerializer.Serialize(group);
            string uri = GenerateUrl(Urls.usersByGroup);
            string jsonL = sendRequest(json, uri, Method.POST);
            return JsonSerializer.Deserialize<ObservableCollection<User>>(jsonL);
        }


        public static ObservableCollection<UserGroup> getUserGroupsByUser(User user)
        {
            string json = JsonSerializer.Serialize(user);
            string uri = GenerateUrl(Urls.userGroupByUser);
            string jsonL = sendRequest(json, uri, Method.POST);
            return JsonSerializer.Deserialize<ObservableCollection<UserGroup>>(jsonL);
        }

        public static ObservableCollection<Test> getTestsByUser(User user)
        {

            string json = JsonSerializer.Serialize(user);
            string uri = GenerateUrl(Urls.testsGetByUser);
            string jsonL = sendRequest(json, uri, Method.POST);
            return JsonSerializer.Deserialize<ObservableCollection<Test>>(jsonL);
         }

        public static ObservableCollection<PassedTest> getPassedestsByUser(User user)
        {
            User mainUser = Client.getUserbyLogin(user.login);
            return mainUser.passedTests;
        }



        //---------------------------------------------GROUP----------------------------------------------------------


        public static List<Group> getAllGroup()
        {
            string uri = GenerateUrl(Urls.groups);
            List<Group> groups = JsonSerializer.Deserialize<List<Group>>(sendRequest("", uri, Method.GET));
            return groups;
        }
        public static string addGroup(Group group)
        {
            string json = JsonSerializer.Serialize(group);
            string uri = GenerateUrl(Urls.groups);
            return sendRequest(json, uri, Method.POST);
        }

        public static string updateGroup(Group group, long id)
        {
            string json = JsonSerializer.Serialize(group);
            string uri = GenerateUrl(Urls.groups + id);
            return sendRequest(json, uri, Method.PUT);
        }

        public static void deleteGroup(long id)
        {
            string uri = GenerateUrl(Urls.groups + "/" + id);
            sendRequest("", uri, Method.DELETE);
        }

        public static Group joinTheGroupByLink(String link)
        {
            try
            {
                string uri = GenerateUrl(Urls.groupsInvitationLink + link);
                string json = sendRequest("", uri, Method.GET);
                return JsonSerializer.Deserialize<Group>(json);
            }
            catch(Exception ee) {
                return null;
            }
           
        }

        //---------------------------------------------USER_GROUP----------------------------------------------------------

        public static string addUserGroup(UserGroup userGroup)
        {
            string json = JsonSerializer.Serialize(userGroup);
            string uri = GenerateUrl(Urls.UserGroups);
            return sendRequest(json, uri, Method.POST);
        }

        public static long getIdByUserAndGroup(UserGroup userGroup)
        {
            string json = JsonSerializer.Serialize(userGroup);
            string uri = GenerateUrl(Urls.UserGroupsGetIdByUserAndGroup);
            return Convert.ToInt32(sendRequest(json, uri, Method.POST));
        }

        public static void deleteuserGroup(long id)
        {
            string uri = GenerateUrl(Urls.UserGroups + "/" + id);
            sendRequest("", uri, Method.DELETE);
        }

        //---------------------------------------------QUESTION----------------------------------------------------------

        public static string addQuestion(Question question)
        {
            string json = JsonSerializer.Serialize(question);
            string uri = GenerateUrl(Urls.questions);
            return sendRequest(json, uri, Method.POST);
        }


        //---------------------------------------------TESTS----------------------------------------------------------

        public static string addTest(Test test)
        {
            string json = JsonSerializer.Serialize(test);
            string uri = GenerateUrl(Urls.tests);
            return sendRequest(json, uri, Method.POST);
        }

        public static string updateTest(Test test, long id)
        {
            string json = JsonSerializer.Serialize(test);
            string uri = GenerateUrl(Urls.tests + id);
            return sendRequest(json, uri, Method.PUT);
        }

        public static string deleteTest(long id)
        {
            string uri = GenerateUrl(Urls.tests + id);
            return sendRequest("", uri, Method.DELETE);
        }


        //---------------------------------------------PASSED TEST----------------------------------------------------------

        public static string addPassedest(PassedTest passedTest)
        {
            string json = JsonSerializer.Serialize(passedTest);
            string uri = GenerateUrl(Urls.passedTests);
            return sendRequest(json, uri, Method.POST);
        }

        //---------------------------------------------GROUP TEST----------------------------------------------------------

        public static string findGroupTestByTest(Test test)
        {
            string json = JsonSerializer.Serialize(test);
            string uri = GenerateUrl(Urls.findGroupTestByTest);
            return sendRequest(json, uri, Method.POST);
        }

    }
}
