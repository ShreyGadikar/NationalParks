﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using NationalParks.Models;


namespace NationalParks.APIHandlerManager
{
    public class APIHandler
    {

        // Obtaining the API key is easy. The same key should be usable across the entire
        // data.gov developer network, i.e. all data sources on data.gov.
        // https://www.nps.gov/subjects/developer/get-started.
        static string BASE_URL = "https://developer.nps.gov/api/v1/";
        static string API_KEY = "WJQC93zgDbpqe5Fzg6lketshMFwj4DjtgjcQD0ug"; //Add your API key here inside 
        HttpClient httpClient;
        /// <summary>
        ///  Constructor to initialize the connection to the data source
        /// </summary>
        public APIHandler()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
    
         
        /// <summary>
        /// Method to receive data from API end point as a collection of objects
        /// 
        /// JsonConvert parses the JSON string into classes
        /// </summary>
        /// <returns></returns>
        public ParksModel GetParks()
        {
            string NATIONAL_PARK_API_PATH = BASE_URL+"parks?parkCode=acad,dena,cabr,cave,grca,jotr,yose,zion,yell,glac";
            string parksData = "";
            ParksModel parks = null;
            //httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_PATH);
            // It can take a few requests to get back a prompt response, if the API has not received
            //  calls in the recent past and the server has put the service on hibernation
            try
            {
                using (var httpClient = new HttpClient { BaseAddress = new Uri(NATIONAL_PARK_API_PATH) })
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_PATH).GetAwaiter().GetResult();
                    parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (!parksData.Equals(""))
                    {

                        // JsonConvert is part of the NewtonSoft.Json Nuget package
                        parks = JsonConvert.DeserializeObject<ParksModel>(parksData);
                    }
                }
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);

            }
                return parks;
            
        }
        public VisitorModel GetVisitorCentres()
        {
            string NATIONAL_PARK_API_PATH = BASE_URL + "visitorcenters?parkCode=acad,dena,cabr,cave,grca,jotr,yose,zion,yell,glac";
            string parksData = "";
            VisitorModel visitors = null;
            //httpClient.BaseAddress = new Uri(NATIONAL_PARK_API_PATH);
            // It can take a few requests to get back a prompt response, if the API has not received
            //  calls in the recent past and the server has put the service on hibernation
            try
            {
                using (var httpClient = new HttpClient { BaseAddress = new Uri(NATIONAL_PARK_API_PATH) })
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = httpClient.GetAsync(NATIONAL_PARK_API_PATH).GetAwaiter().GetResult();
                    parksData = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (!parksData.Equals(""))
                    {

                        // JsonConvert is part of the NewtonSoft.Json Nuget package
                        visitors = JsonConvert.DeserializeObject<VisitorModel>(parksData);
                    }
                }
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);

            }
            return visitors;

        }
    }
}
