🏠 RealEstateApp
By Jonasthepanda
A cross-platform real estate application built with .NET MAUI and C#, using the MVVM architecture.
The app allows users to browse a collection of properties, view detailed property information, interact with agents and vendors, use device features such as location, maps, haptics, TTS, battery and sensors, and manage property-related information through a modern cross-platform UI.
> 🚧 The project currently uses an **in-memory mock repository** rather than a database. Changes made to properties are therefore not persisted after the application is restarted.
Table of Contents
📝 Description
🛠 Tech Stack & Dependencies
💻 Features
📱 Screenshots
🗂️ Project Structure
🏗️ Architecture
💾 Data Storage
🧭 Navigation
🚀 Getting Started
🖥️ Supported Platforms
🎯 Current Tasks & Future Plans
🚧 Known Issues
🐞 Bug Reporting
🔗 Tags & Links
📜 Changelog
👨‍💻 Author
📄 License
---
📝 Description
RealEstateApp is a .NET MAUI application focused on browsing and managing fictional real-estate properties based around Los Santos.
The project started with a collection of GTA V-inspired properties and agents and has gradually expanded into a larger cross-platform application with device integrations, property utilities, contact actions, sharing, authentication, preferences and additional quality-of-life features.
The application is structured around MVVM (Model-View-ViewModel) and uses services to keep application logic separated from the XAML views.
The current development branch is `develop`, and the current project version is V0.8.0.
---
🛠 Tech Stack & Dependencies
Core Technologies
Component	Technology
Language	C#
Framework	.NET 10
UI Framework	.NET MAUI
UI	XAML
Architecture	MVVM
Navigation	.NET MAUI Shell
Dependency Injection	Microsoft.Extensions.DependencyInjection
Data	In-memory mock repository
Location	.NET MAUI Geolocation / Geocoding
Device Features	Connectivity, Haptics, Vibration, Battery, Sensors, Text-to-Speech
Icons	Custom Font Awesome icon font
Fonts	Open Sans
NuGet Packages
The current project configuration includes:
`Microsoft.Maui.Controls` 10.0.31
`Microsoft.Extensions.Logging.Debug` 10.0.2
The project also contains a custom icon font implementation through `Helpers/IconFont.cs`.
---
💻 Features
🏘️ Property Listings
Browse available properties in a scrollable list.
Display property images directly in listings.
View:
Property name
Address
Price
Bedrooms
Bathrooms
Parking capacity
Land size
Distance from the user's location
Pull-to-refresh property data.
Sort properties based on distance.
🔎 Property Details
Each property has a dedicated details page containing information such as:
Property name
Price
Address
Property type
Property tier
Bedrooms
Bathrooms
Parking
Land size
Description
Coordinates
Property images
Listing agent
Agent specialization
Vendors
Contract information
Neighborhood information
Properties can also be edited directly from the details page.
➕ Add & Edit Properties
A shared form is used for creating and editing properties.
Users can manage:
Address
Price
Bedrooms
Bathrooms
Parking
Land size
Description
Listing agent
Location coordinates
The form includes validation and location-based helpers to make entering property information easier.
📍 Location & Geocoding
The application uses device location functionality to:
Retrieve the user's current position.
Calculate distances to properties.
Sort properties by distance.
Automatically retrieve an address from the current location.
Convert an address into latitude and longitude coordinates.
🗺️ Maps
Properties can be opened in a maps application.
The app supports:
Opening a map to a property.
Opening directions to a property.
🌐 Connectivity
The app monitors the device's network connection.
Connectivity is used to:
Detect whether the device is online.
Update the UI when the connection changes.
Prevent or hide location-dependent functionality when a connection is unavailable.
📳 Haptic Feedback & Vibration
Device haptic and vibration functionality is used to provide additional feedback during interactions.
🔊 Text-to-Speech
Property descriptions can be read aloud using the device's Text-to-Speech functionality.
The property details page provides controls to start and stop speech playback.
🔋 Battery Status
The application can read the device's battery status and provide the user with feedback based on the current battery level.
🔦 Flashlight
The application includes a flashlight function for supported devices.
🧭 Compass
The application includes a compass that shows the direction the device is facing and where north is located.
📏 Height Calculator / Barometer
The app includes a barometer-based feature with a save function, along with the related height-calculation functionality.
👤 Agents & Vendors
Properties can be associated with real-estate agents and vendors.
Agent information can include:
Name
Email
Phone number
Description
Website
Specialization
Agent type
Opening hours
Agent image
The app also provides contact actions such as:
Email
SMS
Phone call
📤 Property Sharing
The application supports multiple ways of sharing property information.
📄 Property Contracts
Users can open the contract file associated with a property.
🌍 Neighborhood Information
The application can open a Wikipedia page related to a property's neighborhood.
🔐 Login & Logout
The application includes a login flow with:
Login page
Logout functionality
Stored login state
Navigation that responds to the user's login state
⚙️ Preferences & Settings
The app includes a Settings/Preferences system for storing user preferences so they remain available even after the application is closed.
🎨 UI Improvements
The project includes ongoing styling improvements across multiple pages, with the goal of keeping the application consistent and easier to use across supported platforms.
---
📱 Screenshots
Property List
![Property List](images/PropertiesList.png)
Property Details
![Property Details](images/DetailView.png)
Add Property
![Add Property](images/Add.png)
Edit Property
![Edit Property](images/EditView.png)
Navigation
![Navigation](images/FlyoutMenu.png)
About
![About](images/About.png)
---
🗂️ Project Structure
The application follows an MVVM-oriented structure:
```text
RealEstateApp/
│
├── RealEstateApp/
│   ├── Converters/
│   ├── Helpers/
│   │   └── IconFont.cs
│   │
│   ├── Models/
│   │   ├── Agent.cs
│   │   ├── Property.cs
│   │   ├── PropertyImage.cs
│   │   ├── PropertyListItem.cs
│   │   ├── PropertyTier.cs
│   │   └── PropertyType.cs
│   │
│   ├── Platforms/
│   │   ├── Android/
│   │   ├── iOS/
│   │   ├── MacCatalyst/
│   │   └── Windows/
│   │
│   ├── Properties/
│   ├── Resources/
│   │   ├── AppIcon/
│   │   ├── Fonts/
│   │   ├── Images/
│   │   ├── Raw/
│   │   ├── Splash/
│   │   └── Styles/
│   │
│   ├── Services/
│   │   ├── IPropertyService.cs
│   │   └── MockRepository.cs
│   │
│   ├── ViewModels/
│   │   ├── AddEditPropertyPageViewModel.cs
│   │   ├── BaseViewModel.cs
│   │   ├── PropertyDetailPageViewModel.cs
│   │   └── PropertyListPageViewModel.cs
│   │
│   ├── Views/
│   │   ├── AboutPage.xaml
│   │   ├── AddEditPropertyPage.xaml
│   │   ├── PropertyDetailPage.xaml
│   │   └── PropertyListPage.xaml
│   │
│   ├── App.xaml
│   ├── AppShell.xaml
│   ├── GlobalSettings.cs
│   ├── MauiProgram.cs
│   └── RealEstateApp.csproj
│
├── images/
│   ├── About.png
│   ├── Add.png
│   ├── DetailView.png
│   ├── EditView.png
│   ├── FlyoutMenu.png
│   └── PropertiesList.png
│
├── RealEstateApp.sln
└── README.md
```
---
🏗️ Architecture
The application uses MVVM (Model-View-ViewModel) to separate the UI from presentation logic and data handling.
Models
The model layer contains the application's core data structures:
`Property`
`Agent`
`PropertyImage`
`PropertyListItem`
`PropertyType`
`PropertyTier`
The `Property` model contains information such as pricing, address, description, property type, tier, rooms, parking, land size, agent information, images and coordinates.
Views
The UI is implemented using XAML pages, including:
`PropertyListPage`
`PropertyDetailPage`
`AddEditPropertyPage`
`AboutPage`
ViewModels
The ViewModels contain presentation logic and commands.
`PropertyListPageViewModel`
Responsible for:
Loading properties
Pull-to-refresh
Retrieving location
Calculating distances
Sorting properties
Navigation
Connectivity-related UI state
`PropertyDetailPageViewModel`
Responsible for:
Loading selected properties
Loading related agents and vendors
Property image handling
Editing navigation
Contact actions
Maps functionality
Sharing functionality
Contract access
Neighborhood information
Text-to-Speech functionality
`AddEditPropertyPageViewModel`
Responsible for:
Creating properties
Editing properties
Validating required fields
Agent selection
Location retrieval
Address geocoding
Saving properties
Connectivity-aware location functionality
---
💾 Data Storage
The current version uses an in-memory mock repository rather than a database.
```text
IPropertyService
       │
       ▼
MockRepository
       │
       ├── GetProperties()
       ├── GetAgents()
       └── SaveProperty()
```
New properties are added to the in-memory collection, while existing properties are replaced when they share the same ID.
⚠️ Persistence
Property data changes are currently not persisted between application launches.
Restarting the application reloads the original mock data.
The service abstraction allows a future database or API-backed implementation to replace `MockRepository` without requiring the ViewModels to directly depend on a specific storage implementation.
---
🧭 Navigation
The application uses .NET MAUI Shell navigation.
The main navigation includes:
🏠 Property List
ℹ️ About
🔐 Login
⚙️ Settings
Additional pages and actions are reached through commands and Shell routes.
```text
Login
  │
  └── Property List
          │
          ├── Select Property
          │       └── Property Details
          │               ├── Edit Property
          │               ├── Contact
          │               ├── Share
          │               ├── Contract
          │               ├── Maps
          │               └── Neighborhood
          │
          └── Add Property

Settings
  │
  └── Saved Preferences

About
```
---
🚀 Getting Started
Prerequisites
You will need:
Visual Studio with .NET MAUI support
.NET 10 SDK
A supported .NET MAUI development environment
An Android emulator, iOS simulator, MacCatalyst environment, Windows machine, or compatible physical device depending on the target platform
Clone the Repository
```bash
git clone https://github.com/Jonasthepanda67/RealEstateApp.git
cd RealEstateApp
```
Open the Project
Open:
```text
RealEstateApp.sln
```
Select your desired target platform and run the application.
---
🖥️ Supported Platforms
The current project configuration targets:
Android
iOS
macOS via MacCatalyst
Windows
Tizen support is present in the project configuration as commented-out code and is not currently enabled.
---
🎯 Current Tasks & Future Plans
[ ] Persistent database storage
[ ] Cloud/API-backed property listings
[ ] User accounts with a proper backend
[ ] Favorites / saved properties
[ ] Advanced property filtering
[ ] Search functionality
[ ] Interactive in-app map view
[ ] Property image uploading
[ ] Agent/vendor management
[ ] Property deletion
[ ] Property type and tier selection in the add/edit form
[ ] Improved validation
[ ] Offline data persistence
[ ] Additional platform-specific functionality
---
🚧 Known Issues
No major known issues are currently documented.
The main architectural limitation is that property data is stored in memory and therefore resets when the application is restarted.
---
🐞 Bug Reporting
Bug reporting is not currently enabled for this project.
If you encounter an issue, feel free to open an issue on GitHub or make your own improvements to the project.
---
🔗 Tags & Links
Versions are based around the version numbers used for the project's pull requests.
Tags are intended to be created after each version has been merged and finalized.
Version	Pull Request(s)	Tag
V0.1.0	#1, #2	V0.1.0
V0.2.0	#3, #4	V0.2.0
V0.3.0	#5, #6	V0.3.0
V0.4.0	#7, #8	V0.4.0
V0.5.0	#12	V0.5.0
V0.6.0	#13, #14, #15	V0.6.0
V0.7.0	#16, #17	V0.7.0
V0.8.0	#18, #19	V0.8.0
> **Current version:** `V0.8.0`  
> The `V0.8.0` tag is prepared above and can be created once the current version is finalized.
---
📜 Changelog
V0.8.0
Added login functionality.
Added logout functionality.
Added persistent Preferences/Settings for user preferences.
Improved styling across multiple pages.
Added and refreshed application screenshots.
Continued improving the application experience around stored user state.
V0.7.0
Added Wikipedia browser functionality for property neighborhoods.
Added functionality for opening property contract files.
Added multiple ways to share property information.
V0.6.0
Added vendor support to properties.
Added vendors to the property details page.
Added email, SMS and phone contact functionality.
Added maps functionality.
Added the ability to open a map for a property.
Added directions functionality.
V0.5.0
Added a barometer feature.
Added the ability to save barometer information.
V0.4.0
Added Text-to-Speech for property descriptions.
Added start/stop controls for Text-to-Speech.
Added battery status functionality.
Added battery-level feedback.
Added flashlight functionality.
V0.3.0
Added connectivity detection.
Added connection-aware UI behaviour.
Disabled or hid location-dependent functionality when a connection is unavailable.
Added haptic feedback.
Added vibration functionality.
V0.2.0
Added current-location retrieval.
Added property distance calculations.
Added property sorting by distance.
Added geocoding functionality.
Added automatic address handling based on the user's current location.
Added multiple location-related quality-of-life improvements.
V0.1.0
Added GTA V-inspired properties and agents.
Added additional property details to the models.
Added the initial real-estate property and agent data foundation.
---
👨‍💻 Author
Jonasthepanda
GitHub: @Jonasthepanda67
---
📄 License
This project does not currently specify a license.
