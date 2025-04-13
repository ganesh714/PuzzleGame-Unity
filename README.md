# 🧩 Puzzle Hunter - Unity Game

An interactive and engaging puzzle game built with Unity. Featuring 20+ logic-based levels, a hint system, and rewarded ad integration to enhance user experience. Designed for smooth performance and mobile play.

![Engine](https://img.shields.io/badge/Engine-Unity-000?logo=unity&logoColor=white)
![Platform](https://img.shields.io/badge/Platform-Android-green)
![Ads](https://img.shields.io/badge/Ads-Google%20Mobile%20Ads-yellow)

---

## 🚀 Features

- 🎮 20+ handcrafted levels with increasing difficulty
- 💡 Hint and skip systems powered by rewarded ads
- 🎨 Clean and responsive UI (designed using Photoshop)
- 🔊 Interactive sounds for actions and feedback
- 📱 Optimized for mobile performance

---

## 📦 Project Structure

```
Assets/
├── Animation/              # Animations & controllers for hint, skip, etc.
├── GoogleMobileAds/        # AdMob SDK and libraries
├── Plugins/                # Android & iOS ad plugin files
├── Scenes/                 # MainMenu, Season1, and game scenes
├── Scripts/                # Game logic and ad control scripts
├── Sounds/                 # SFX like click, correct answer, etc.
├── Sprites/                # Puzzle graphics and UI assets
```

---

## 📸 Screenshots

_Add a few screenshots or gameplay GIFs here to show off the game!_

---

## 🛠️ Tech Stack

- **Unity** (2021.x or higher)
- **Android SDK**
- **C#**
- **Photoshop** – for UI/graphic design
- **Google AdMob SDK** – for ads (rewarded and banner)

---

## 📥 How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/ganesh714/PuzzleGame-Unity.git
   ```

2. Open the project in **Unity Hub**  
   _(Recommended: Unity 2021.x or newer)_

3. Open the `MainMenu.unity` scene from the `Scenes/` folder

4. Press ▶️ **Play** in Unity Editor to start

---

## 🧠 Key Scripts

- `BannerAds.cs` – Controls banner ads
- `RewardedAdsInMM.cs` / `RewardedAdsInS1.cs` – Rewarded ads for hints/skips
- `HintManager.cs`, `SkipManager.cs` – Manage user access to puzzle help
- `S1GameController.cs`, `S1LevelsController.cs` – Core gameplay logic

---

## 🙋‍♂️ Author

**Veera Venkata Ganesh Elisetti**  
🔗 [GitHub Profile](https://github.com/ganesh714)

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).

---

⭐️ _If you like this project, give it a star! It helps a lot._
