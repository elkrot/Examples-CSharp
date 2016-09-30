// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
using System;
using System.Collections.Generic;
using LinqToTerraServerProvider.TerraServerReference;

[assembly: CLSCompliant(true)]
namespace LinqToTerraServerProvider
{
    internal static class WebServiceHelper
    {
        private static int numResults = 200;
        private static bool mustHaveImage = false;

        internal static Place[] GetPlacesFromTerraServer(List<string> locations)
        {
            // Ограничить общее количество вызовов веб-службы.
            if (locations.Count > 5)
                throw new InvalidQueryException("This query requires more than five separate calls to the Web service. Please decrease the number of locations in your query.");

            List<Place> allPlaces = new List<Place>();

            // Для каждого расположения вызовите веб-службу для получения данных.
            foreach (string location in locations)
            {
                Place[] places = CallGetPlaceListMethod(location);
                allPlaces.AddRange(places);
            }

            return allPlaces.ToArray();
        }

        private static Place[] CallGetPlaceListMethod(string location)
        {
            TerraServiceSoapClient client = new TerraServiceSoapClient();
            PlaceFacts[] placeFacts = null;

            try
            {
                // Вызвать метод веб-службы "GetPlaceList".
                placeFacts = client.GetPlaceList(location, numResults, mustHaveImage);

                // Если есть результаты 'numResults', то они, вероятно, отбрасываются.
                if (placeFacts.Length == numResults)
                    throw new InvalidQueryException("The results have been truncated by the Web service and would not be complete. Please try a different query.");

                // Создать объекты Place из объектов PlaceFacts, возвращенных веб-службой.
                Place[] places = new Place[placeFacts.Length];
                for (int i = 0; i < placeFacts.Length; i++)
                {
                    places[i] = new Place(
                        placeFacts[i].Place.City,
                        placeFacts[i].Place.State,
                        placeFacts[i].PlaceTypeId);
                }

                // Закрыть клиент WCF.
                client.Close();

                return places;
            }
            catch (TimeoutException)
            {
                client.Abort();
                throw;
            }
            catch (System.ServiceModel.CommunicationException)
            {
                client.Abort();
                throw;
            }
        }
    }
}
