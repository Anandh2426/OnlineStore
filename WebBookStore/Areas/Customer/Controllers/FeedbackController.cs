using BookStore.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

[Area("Customer")]
[Authorize]
public class FeedbackController : Controller
{
    private readonly HttpClient _httpClient;
    Uri baseAddress = new Uri("https://localhost:7077/api");
    public FeedbackController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = baseAddress;
    }
    [HttpGet]
    public IActionResult Index()
    {
        List<FeedbackVM> feedbackList = new List<FeedbackVM>();
        HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Feedbacks/GetFeedbacks").Result;

        if (response.IsSuccessStatusCode)
        {
            string data = response.Content.ReadAsStringAsync().Result;
            feedbackList = JsonConvert.DeserializeObject<List<FeedbackVM>>(data);
        }
        return View(feedbackList);
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(FeedbackVM model)
    {
        try
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Feedbacks/AddFeedbacks", content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Feedback Added";
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Invalid Id";
            TempData["errorMessage"] = ex.Message;
            return View();
        }
        return View();
    }
    [HttpGet]

    public IActionResult Edit(int Id)
    {
        try
        {
            FeedbackVM feedback = new FeedbackVM();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Feedbacks/SearchFeedback" + Id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                feedback = JsonConvert.DeserializeObject<FeedbackVM>(data);
            }
            return View(feedback);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View();
        }
    }
    [HttpPost]

    public IActionResult Edit(FeedbackVM model)
    {
        try
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/Feedbacks/UpdateFeedback", content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Feedback Updated";
                return RedirectToAction("Index");
            }
            return View();
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View();
        }
    }
    [HttpDelete]
    public IActionResult Delete(int Id)
    {
        try
        {
            FeedbackVM feedback = new FeedbackVM();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Feedbacks/SearchFeedback" + Id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                feedback = JsonConvert.DeserializeObject<FeedbackVM>(data);
            }
            return View(feedback);
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View();
        }
    }
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int Id)
    {
        try
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress + "/Feedbacks/DeleteFeedback" + Id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["successMessage"] = "Feedback Deleted";
                return RedirectToAction("Index");
            }
            return View();
        }
        catch (Exception ex)
        {
            TempData["errorMessage"] = ex.Message;
            return View();
        }
    }
}