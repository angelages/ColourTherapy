# 🎨 Colour Therapy

A Blazor WebAssembly application that provides personalized colour and flower suggestions based on the user's mood.

## 📝 Description

Colour Therapy helps users discover colours and flowers that can positively impact their emotional state. By selecting a mood, users receive recommendations for colours that may help balance or enhance their emotional wellbeing, along with matching flowers that complement these colours.

## ✨ Features

- **Mood Selection**: Choose from a variety of moods to get personalized recommendations
- **Colour Suggestions**: Receive colour recommendations tailored to your selected mood
- **Flower Suggestions**: Discover flowers that match your recommended colours
- **Results Summary**: View a comprehensive summary of your mood, recommended colours, and flowers

## 🛠️ Technologies Used

- **[.NET 9.0](https://dotnet.microsoft.com/)**: The application framework
- **[Blazor WebAssembly](https://dotnet.microsoft.com/apps/aspnet/web-apps/blazor)**: For client-side web UI
- **[Newtonsoft.Json](https://www.newtonsoft.com/json)**: For JSON data handling
- **[Bootstrap](https://getbootstrap.com/)**: For responsive layout and styling

## 📋 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- A modern web browser that supports WebAssembly

## 🚀 Getting Started

1. **Clone the repository**

```bash
git clone https://github.com/yourusername/ColourTherapy.git
cd ColourTherapy
```

2. **Restore dependencies**

```bash
dotnet restore
```

3. **Build and run the application**

```bash
dotnet build
cd ColourTherapy
dotnet run
```

4. **Open your browser** and navigate to `https://localhost:5001` or the URL displayed in your terminal

## 📂 Project Structure

- **Models/**: Contains data models for moods, colours, flowers, and therapy results
- **Pages/**: Razor pages for the application UI
- **Services/**: Contains the TherapyService for data retrieval and processing
- **wwwroot/data/**: JSON data files for moods, colours, and flowers
- **wwwroot/css/**: Custom CSS styles
- **wwwroot/js/**: JavaScript files for client-side functionality

## 🧩 How It Works

1. The user selects their current mood from the available options
2. The application retrieves colour recommendations based on the selected mood
3. Flower suggestions are generated based on the recommended colours
4. A summary of the selections and recommendations is displayed

## 📊 Data Structure

The application uses JSON files to store mood, colour, and flower data:

- **moods.json**: Contains mood options with IDs, names, and descriptions
- **colours.json**: Maps mood IDs to corresponding colour recommendations
- **flowers.json**: Contains flower information with colour matching data

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 📧 Contact

Leo Harker - leo.harker@gmail.com

Project Link: [https://github.com/angelages/FlowerColourTherapy](https://github.com/angelages/FlowerColourTherapy)

---

Made with ❤️ and C#
