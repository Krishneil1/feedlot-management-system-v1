# 🐄 Feedlot App – Proof of Concept

**Date:** 6 July 2025  
**Developer:** Krishneel Kumar  
**Project Type:** Rapid Prototype / Proof of Concept  
**Development Time:** ~10 hours over 2 days  

## 📌 Overview

Feedlot App is a cross-platform mobile application built using **.NET MAUI**, with a **.NET 8 Web API** backend. It enables offline livestock booking creation, with automatic synchronization to a remote server when network becomes available. This project was built as a **proof of concept**, demonstrating offline-first architecture and sync logic using .NET.

---

## 📱 Tested Devices & Environment

| Component       | Details                              |
|----------------|---------------------------------------|
| Device          | Physical Android smartphone (debug)   |
| Backend         | .NET 8 Web API                        |
| Frontend        | .NET MAUI (Android)                   |
| Local Database  | SQLite (`SQLiteAsyncConnection`)      |
| Sync Protocol   | REST (POST/PUT with GUID-based sync)  |

---

## ✅ Features Implemented

- 📋 Local creation of bookings (with unique `PublicId` GUID)
- 📴 Offline-first capability with local SQLite storage
- 🔄 Automatic sync to remote API when online
- 📡 Smart PUT/POST sync logic based on GUID
- 🧭 MAUI Shell navigation and side menu
- 📝 Edit & delete bookings with responsive scrollable UI
- 🐞 Basic error logging using `Debug.WriteLine`

---

## ⚠️ Known Limitations

| Area               | Limitation Description                                                                 |
|--------------------|-----------------------------------------------------------------------------------------|
| 🧪 Testing          | No unit tests or integration tests; manual testing only                                |
| 🏗️ Prod Readiness   | No retry logic, secure logging, or production-grade error handling                     |
| 🔐 Security         | No authentication, token usage, or encrypted comms                                     |
| ⚠️ Error Handling   | Minimal try/catch logic, no feedback to user                                            |
| 🔄 Conflict Handling| No merge logic for offline edits clashing with server updates                          |
| 🐄 Animal Sync      | Animal details are not synced with booking yet                                          |
| ❌ UX               | No confirm dialog for delete; limited validation                                        |
| ⚙️ Configuration    | API base URL and DB connection string must be manually updated in config/code          |

---

## 🛠 Developer Setup Instructions

### Backend Setup (FeedlotApi)

1. Ensure you have **.NET 8 SDK** installed.
2. Create and apply **EF Core migrations**.
3. Update `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=192.168.68.127,9433;Database=feedlot;User Id=sa;Password=P@ssword!;TrustServerCertificate=true"
  },
  "ApiBaseUrl": "http://192.168.68.100:5282"
}
```
4. Run the Web API:
```bash
dotnet run --project FeedlotApi
```

### Frontend Setup (FeedlotApp)

1. Open in Visual Studio with Android device/emulator.
2. Update `SyncService.cs` to match backend API base URL.
3. Launch the app in **Debug mode**.

---

## 🧪 Notes

- This project is for demonstration purposes and is not intended for production use.
- Built and tested by **Krishneel Kumar** using real device testing and local network sync.

---

## 📬 Feedback & Contributions

Pull requests and issues are welcome! Please fork this repository or contact the developer for more details.