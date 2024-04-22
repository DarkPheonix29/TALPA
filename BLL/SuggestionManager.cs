using BLL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SuggestionManager
    {
        private readonly SuggestionDataManager _suggestionDataManager;
        public SuggestionManager()
        {
            _suggestionDataManager = new SuggestionDataManager();
        }

        public bool SubmitSuggestion(string user, string suggestion, string description, List<string> limitations, List<string> categories)
        {
            string time = (DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
            int suggestion_id = _suggestionDataManager.SubmitSuggestion(user, suggestion, description, time);
            foreach (string limitation in limitations)
            {
                _suggestionDataManager.SubmitLimitation(suggestion_id, limitation, "-");
            }

            foreach (string categorie in categories)
            {
                Console.WriteLine(categorie);
                _suggestionDataManager.SubmitCategorie(suggestion_id, categorie, "-");
            }

            return suggestion_id != null;
        }

        public List<Suggestion> GetUserSuggestions(string user)
        {
            List<Suggestion> suggestions = new List<Suggestion>();
            DataTable result = _suggestionDataManager.GetUserSuggestions(user);
            foreach (DataRow row in result.Rows)
            {
                List<string> limitations = new List<string>();
                List<string> categories = new List<string>();
                suggestions.Add(new Suggestion(row["suggestion"].ToString(), row["description"].ToString(), limitations, categories, 0, 0, 0));
            }

            return suggestions;
        }

        public List<Suggestion> GetSuggestions()
        {
            List<Suggestion> suggestions = new List<Suggestion>();
            DataTable result = _suggestionDataManager.GetSuggestions();
            foreach (DataRow row in result.Rows)
            {
                List<string> limitations = new List<string>();
                DataTable result2 = _suggestionDataManager.GetSuggestionLimitations(Convert.ToInt32(row["id"]));
                foreach (DataRow row2 in result2.Rows)
                {
                    limitations.Add(row2["limitation"].ToString());
                }

                List<string> categories = new List<string>();
                DataTable result3 = _suggestionDataManager.GetSuggestionCategories(Convert.ToInt32(row["id"]));
                foreach (DataRow row3 in result3.Rows)
                {
                    categories.Add(row3["categorie"].ToString());
                }

                suggestions.Add(new Suggestion(row["suggestion"].ToString(), row["description"].ToString(), limitations, categories, 1, 2, 3));
            }

            return suggestions;
        }
    }
}
