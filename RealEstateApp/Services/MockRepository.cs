using RealEstateApp.Models;
using RealEstateApp.Services;

namespace RealEstateApp.Repositories
{
    public class MockRepository : IPropertyService
    {
        public MockRepository()
        {
            LoadProperties();
            LoadAgents();
        }

        private List<Agent> _agents;
        private List<Property> _properties;

        public List<Agent> GetAgents() => _agents;
        public List<Property> GetProperties() => _properties;

        public void SaveProperty(Property property)
        {
            if (property.Id == null) throw new NullReferenceException("Property.Id cannot be null");

            var existing = _properties.FirstOrDefault(x => x.Id == property.Id);

            if (existing == null)
            {
                _properties.Add(property);
            }
            else
            {
                var existingIndex = _properties.IndexOf(existing);

                _properties[existingIndex] = property;
            }
        }


        private void LoadProperties()
        {
            _properties = new List<Property>
            {
                new Property
                {
                    Id = "property_1", Name = "Richman Villa",
                    Address = "905 Loma Vista Drive, Beverly Hills, CA 90210",
                    Description = "The Richman Villa replaces the former Ace Jones Drive Overlook and sits perched on a prime, high-altitude cliffside plot in the Vinewood Hills / Richman border zone of North Los Santos.",
                    Type = PropertyType.MANSION, Tier = PropertyTier.LEGENDARY,
                    Beds = 1, Baths = 1, Parking = 20, LandSize = 16000, Price = 12800000, AgentId = "agent_prix_luxury_real_estate",
                    ImageUrls = GetPropertyImageUrls("richman_villa"),
                    Latitude = 34.092075, Longitude = -118.401588
                },
                new Property
                {
                    Id = "property_2", Name = "Maze Bank West Office",
                    Address = "401 Wilshire Boulevard, Santa Monica, CA 90401",
                    Description = "Located at the vibrant corner of Marathon Avenue and Prosperity Street. It is the lowest to the ground of most offices, offering a quick helicopter landing. It sits immediately adjacent to the Del Perro subway station entrance and looks directly over the movie sets of Backlot City.",
                    Type = PropertyType.OFFICE, Tier = PropertyTier.PREMIUM,
                    Beds = 1, Baths = 1, Parking = 60, LandSize = 25000, Price = 1000000, AgentId = "agent_dynasty_8_executive",
                    ImageUrls = GetPropertyImageUrls("maze_bank_west_office"),
                    Latitude =  34.019893, Longitude = -118.498653
                },
                new Property
                {
                    Id = "property_3", Name = "Maze Bank Tower Office",
                    Address = "633 West 5th Street, Los Angeles, CA 90071",
                    Description = "The literal center point of the Los Santos skyline. Standing as the tallest skyscraper on the map, its helipad requires the highest altitude climb to reach. The street-level approach features an iconic, massive concrete plaza with winding fountains and escalators, making it a high-traffic zone for people.",
                    Type = PropertyType.OFFICE, Tier = PropertyTier.LEGENDARY,
                    Beds = 1, Baths = 1, Parking = 60, LandSize = 60000, Price = 4000000, AgentId = "agent_dynasty_8_executive",
                    ImageUrls = GetPropertyImageUrls("maze_bank_tower_office"),
                    Latitude = 34.051051, Longitude = -118.254413
                },
                new Property
                {
                    Id = "property_4", Name = "Lombank West Office",
                    Address = "100 Wilshire Boulevard, Santa Monica, CA 90401",
                    Description = "Situated right at the intersection of Boulevard Del Perro and Bay City Avenue. This beachside office is famous among people because its floor-to-ceiling windows look straight out over the Del Perro Pier, the beach, and the Pacific Ocean, giving it the undisputed best view in the city.",
                    Type = PropertyType.OFFICE, Tier = PropertyTier.PREMIUM,
                    Beds = 1, Baths = 1, Parking = 60, LandSize = 35000, Price = 3100000, AgentId = "agent_dynasty_8_executive",
                    ImageUrls = GetPropertyImageUrls("lombank_west_office"),
                    Latitude = 34.016716, Longitude = -118.500626,
                },
                new Property
                {
                    Id = "property_5", Name = "Arcadius Business Center Office",
                    Address = "11845 West Olympic Boulevard, Los Angeles, CA 90064",
                    Description = "Located in the heart of Downtown Los Santos on Alta Street. It features a highly recognizable circular glass design. Its defining feature is a heavily protected underground parking garage entry loop, meaning people can drive inside out of the sightlines of hostile competitors on the surface.",
                    Type = PropertyType.OFFICE, Tier = PropertyTier.PREMIUM,
                    Beds = 1, Baths = 1, Parking = 60, LandSize = 45000, Price = 2250000, AgentId = "agent_dynasty_8_executive",
                    ImageUrls = GetPropertyImageUrls("arcadius_business_center_office"),
                    Latitude = 34.032887, Longitude = -118.45133,
                },
                new Property
                {
                    Id = "property_6", Name = "Eclipse Towers, Penthouse Suite 1",
                    Address = "9255 Doheny Road, West Hollywood, CA 90069",
                    Description = "A minimalist open-concept suite facing east toward the Sunset Strip and Hollywood Hills. Features floor-to-ceiling glass walls, a designer chef's kitchen, and a wraparound terrace built for entertaining.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.LEGENDARY,
                    Beds = 2, Baths = 2, Parking = 2, LandSize = 6000, Price = 985000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("eclipse_towers_penthouse_suite_1"),
                    Latitude = 34.090898, Longitude = -118.394092,
                },
                new Property
                {
                    Id = "property_7", Name = "Eclipse Towers, Penthouse Suite 2",
                    Address = "9255 Doheny Road, West Hollywood, CA 90069",
                    Description = "An executive-styled luxury unit facing west over Beverly Hills and Bel-Air. Includes a private elevator foyer, a home wellness gym, a glass wine wall, and an imported stone fireplace",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.LEGENDARY,
                    Beds = 3, Baths = 4, Parking = 3, LandSize = 7500, Price = 905000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("eclipse_towers_penthouse_suite_2"),
                    Latitude = 34.090898, Longitude = -118.394092,
                },
                new Property
                {
                    Id = "property_8", Name = "Eclipse Towers, Penthouse Suite 3",
                    Address = "9255 Doheny Road, West Hollywood, CA 90069",
                    Description = "The ultimate full-floor crown jewel offering 360-degree views of the entire LA basin to the Pacific Ocean. Features an oversized master sanctuary, dual dressing rooms, a catering kitchen, and smart-home automation.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.LEGENDARY,
                    Beds = 3, Baths = 6, Parking = 4, LandSize = 12900, Price = 1100000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("eclipse_towers_penthouse_suite_3"),
                    Latitude = 34.090898, Longitude = -118.394092,
                },
                new Property
                {
                    Id = "property_9", Name = "Tinsel Towers, Apt. 42",
                    Address = "9255 Doheny Road, West Hollywood, CA 90069",
                    Description = "A contemporary, wide-format lateral condo featuring custom granite countertops, a floating fireplace, and automated floor-to-ceiling glass panel windows. Positioned to capture an elevated, energetic urban view looking directly over the Sunset Strip nightlife and the glowing West Hollywood grid.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 2, Baths = 2, Parking = 2, LandSize = 3600, Price = 492000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("tinsel_towers_apt_42"),
                    Latitude = 34.090898, Longitude = -118.394092,
                },
                new Property
                {
                    Id = "property_10", Name = "Richards Majestic, Apt. 2",
                    Address = "10250 Constellation Boulevard, Century City, Los Angeles, CA 90067",
                    Description = " An ultra-premium, high-ceiling luxury apartment blending classic Hollywood design with modern structural glass. Located in a dominant 35-story Century City skyscraper, the custom unit offers panoramic views looking out over the neighboring major movie production studios, corporate high-rises, and the distant coastline.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 3, Baths = 3, Parking = 3, LandSize = 4200, Price = 484000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("richards_majestic_apt_2"),
                    Latitude = 34.057045, Longitude = -118.417508,
                },
                new Property
                {
                    Id = "property_11", Name = "Eclipse Towers, Apt. 3",
                    Address = "9255 Doheny Road, West Hollywood, CA 90069",
                    Description = "A lower-tier architectural masterpiece sitting right on the border of Beverly Hills. Features an open-concept layout with floor-to-ceiling glass walls that look out at eye-level over the energetic palm trees and neon lights of the Sunset Strip.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 2, Baths = 2, Parking = 2, LandSize = 3100, Price = 500000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("eclipse_towers_apt_3"),
                    Latitude = 34.090898, Longitude = -118.394092,
                },
                new Property
                {
                    Id = "property_12", Name = "Del Perro Heights, Apt. 4",
                    Address = "10501 Wilshire Boulevard, Los Angeles, CA 90024",
                    Description = "A premium beach-adjacent condo situated along the prestigious Wilshire Corridor. Designed with a modern nautical theme, it features marble floors, custom smart-home tech, and floor-to-ceiling windows capturing sweeping views of the Santa Monica coastline and the Pacific Ocean.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 2, Baths = 3, Parking = 2, LandSize = 3400, Price = 468000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("del_perro_heights_apt_4"),
                    Latitude = 34.064163, Longitude = -118.432364,
                },
                new Property
                {
                    Id = "property_13", Name = "4 Integrity Way, Apt. 28",
                    Address = "1000 Wilshire Boulevard, Los Angeles, CA 90017",
                    Description = "A high-ceiling, modern executive suite located inside a striking 21-story granite and glass office tower. Positioned in the heart of Downtown LA, its massive panoramic windows offer a dramatic, metropolitan view overlooking bustling freeway loops and the financial district skyline.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 3, Baths = 3, Parking = 3, LandSize = 38000, Price = 476000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("integrity_way_4_apt_28"),
                    Latitude = 34.051474, Longitude = -118.261657,
                },
                new Property
                {
                    Id = "property_14", Name = "Weazel Plaza, Apt. 101",
                    Address = "2121 Avenue of the Stars, Century City, Los Angeles, CA 90067",
                    Description = "The ultimate mid-century corporate-style penthouse. Offers breathtaking, unobstructed 360-degree views stretching across Century City, the sprawling LA basin, and the ocean.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 3, Baths = 4, Parking = 3, LandSize = 5200, Price = 335000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("weazel_plaza_apt_101"),
                    Latitude = 34.0550656, Longitude = -118.4134437,
                },
                new Property
                {
                    Id = "property_15", Name = "Weazel Plaza, Apt. 70",
                    Address = "2121 Avenue of the Stars, Century City, Los Angeles, CA 90067",
                    Description = "Fully customized with floating glass walls and a professional chef's kitchen. Faces westward to capture premium evening sunsets sinking behind the hills of Bel-Air and Beverly Hills.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.COMMON,
                    Beds = 2, Baths = 2, Parking = 3, LandSize = 4100, Price = 319000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("weazel_plaza_apt_70"),
                    Latitude = 34.0550656, Longitude = -118.4134437,
                },
                new Property
                {
                    Id = "property_16", Name = "Weazel Plaza, Apt. 26",
                    Address = "2121 Avenue of the Stars, Century City, Los Angeles, CA 90067",
                    Description = "A sleek, minimalist apartment featuring custom concrete textures and industrial lighting. Looks directly down onto the immaculately landscaped plazas and modern corporate high-rises below.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.COMMON,
                    Beds = 2, Baths = 2, Parking = 3, LandSize = 3500, Price = 304000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("weazel_plaza_apt_26"),
                    Latitude = 34.0550656, Longitude = -118.4134437,
                },
                new Property
                {
                    Id = "property_17", Name = "Tinsel Towers, Apt. 45",
                    Address = "9255 Doheny Road, West Hollywood, CA 90069",
                    Description = "A stunning, light-filled luxury unit featuring custom white-marble countertops and automated Fleetwood glass doors. Sits at a high vantage point overlooking the entire, glowing West Hollywood street grid.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 2, Baths = 2, Parking = 2, LandSize = 3800, Price = 270000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("tinsel_towers_apt_45"),
                    Latitude = 34.0908979, Longitude = -118.39409218,
                },
                new Property
                {
                    Id = "property_18", Name = "Tinsel Towers, Apt. 29",
                    Address = "9255 Doheny Road, West Hollywood, CA 90069",
                    Description = "A cozy yet hyper-modern layout anchored by a custom central fireplace and built-in wine wall. Features an extended private concrete terrace looking directly up at the exclusive multi-million dollar estates of the Hollywood Hills.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 2, Baths = 2, Parking = 2, LandSize = 3400, Price = 286000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("tinsel_towers_apt_29"),
                    Latitude = 34.0908979, Longitude = -118.39409218,
                },
                new Property
                {
                    Id = "property_19", Name = "The Diamond Casino Penthouse",
                    Address = "888 Bicycle Casino Drive, Bell Gardens, CA 90201",
                    Description = "A sprawling, fully customized luxury sky estate sitting atop a premier hotel-casino. Features an open-concept media lounge, private cinema room, a fully-stocked bar cluster with a retro arcade alcove, a high-limit poker room, an indoor infinity plunge pool, and a private helicopter pad terrace.",
                    Type = PropertyType.PENTHOUSE, Tier = PropertyTier.LEGENDARY,
                    Beds = 1, Baths = 3, Parking = 10, LandSize = 14500, Price = 6533500, AgentId = "agent_diamond_casino_resort",
                    ImageUrls = GetPropertyImageUrls("the_diamond_casino_penthouse"),
                    Latitude = 33.932804, Longitude = -118.16423,
                },
                new Property
                {
                    Id = "property_20", Name = "Richards Majestic, Apt. 51",
                    Address = "10250 Constellation Boulevard, Century City, Los Angeles, CA 90067",
                    Description = "A fully renovated panoramic condo sitting on the building's highest residential tier. Built with custom tinted glass, it faces west to offer exceptional views looking over Beverly Hills all the way down to the Santa Monica Pier wheel lighting up at night.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 3, Baths = 3, Parking = 10, LandSize = 4500, Price = 253000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("richards_majestic_apt_51"),
                    Latitude = 34.0570451, Longitude = -118.417508,
                },
                new Property
                {
                    Id = "property_21", Name = "Richards Majestic Apt. 4",
                    Address = "10250 Constellation Boulevard, Century City, Los Angeles, CA 90067",
                    Description = " A mid-level architectural lateral unit featuring custom exposed wood paneling and minimalist concrete accents. Its layout focuses on city-center sightlines, looking straight into the sleek high-rises and pristine corporate structures of Century City.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 3, Baths = 3, Parking = 10, LandSize = 4100, Price = 241000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("richards_majestic_apt_4"),
                    Latitude = 34.0570451, Longitude = -118.417508,
                },
                new Property
                {
                    Id = "property_22", Name = "Eclipse Towers, Apt. 40",
                    Address = "9255 Doheny Road, West Hollywood, CA 90069",
                    Description = "A stunning, ultra-modern penthouse sitting on a premier high tier. The layout is customized with monochromatic marble, floating architectural walls, and motorized glass sliders that transition smoothly out onto a wraparound terrace. It captures a dominant, sweeping view looking directly down over the bright lights of the Sunset Strip.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 2, Baths = 2, Parking = 10, LandSize = 3800, Price = 391000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("eclipse_towers_apt_40"),
                    Latitude = 34.0908979, Longitude = -118.3940921,
                },
                new Property
                {
                    Id = "property_23", Name = "Eclipse Towers, Apt. 31",
                    Address = "9255 Doheny Road, West Hollywood, CA 90069",
                    Description = "A uniquely customized mid-high floor unit utilizing a rare interior split-level glass balcony template. Designed with natural white oak wood and brushed limestone, this unit is oriented westward to capture dramatic, clean panoramic views stretching over the green hills of Beverly Hills and Bel-Air.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 3, Baths = 3, Parking = 10, LandSize = 4100, Price = 400000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("eclipse_towers_apt_31"),
                    Latitude = 34.0908979, Longitude = -118.3940921,
                },
                new Property
                {
                    Id = "property_24", Name = "Eclipse Towers, Apt. 9",
                    Address = "9255 Doheny Road, West Hollywood, CA 90069",
                    Description = "A highly detailed modern suite positioned on a lower residential tier. Features high-impact concrete textures, custom integrated LED ambient lighting, and an expansive chef's kitchen. Because it sits lower in the tower, the floor-to-ceiling glass offers a kinetic, immersive view framing the vibrant palm trees and street-level energy of the West Hollywood border.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 2, Baths = 2, Parking = 10, LandSize = 3100, Price = 373000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("eclipse_towers_apt_9"),
                    Latitude = 34.0908979, Longitude = -118.3940921,
                },
                new Property
                {
                    Id = "property_25", Name = "Eclipse Towers, Apt. 5",
                    Address = "9255 Doheny Road, West Hollywood, CA 90069",
                    Description = " A highly personalized high-end unit situated on a mid-level residential floor. The custom glass-paneled grand salon overlooks the palm tree line of the Sunset Strip, offering direct views of the West Hollywood layout below.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 2, Baths = 2, Parking = 10, LandSize = 3100, Price = 382000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("eclipse_towers_apt_5"),
                    Latitude = 34.0908979, Longitude = -118.3940921,
                },
                new Property
                {
                    Id = "property_26", Name = "Dream Tower, Apt. 15",
                    Address = "616 South Normandie Avenue, Los Angeles, CA 90005",
                    Description = "A fully customized medium-end apartment inside a prominent 13-story residential tower located in Koreatown (Little Seoul). Features an open-concept living space with large windows looking out over the local commercial avenues and the building's private terrace pool deck.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.COMMON,
                    Beds = 2, Baths = 1, Parking = 60, LandSize = 2400, Price = 134000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("dream_tower_apt_15"),
                    Latitude = 34.0631864, Longitude = -118.300259,
                },
                new Property
                {
                    Id = "property_27", Name = "Del Perro Heights, Apt. 7",
                    Address = "233 Wilshire Boulevard, Santa Monica, CA 90401",
                    Description = "A lower-tier high-end condo located inside a prime Santa Monica-style complex. Positioned on a lower floor tier, its panoramic glass windows provide a street-level vantage point looking onto the vibrant tech corridors and office blocks of the neighborhood.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.COMMON,
                    Beds = 2, Baths = 2, Parking = 10, LandSize = 3400, Price = 200000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("del_perro_heights_apt_7"),
                    Latitude = 34.0192694, Longitude = -118.4981403,
                },
                new Property
                {
                    Id = "property_28", Name = "Del Perro Heights, Apt. 20",
                    Address = "233 Wilshire Boulevard, Santa Monica, CA 90401",
                    Description = "A top-tier luxury layout featuring custom limestone and wood floor treatments. Perched on a higher floor configuration, it looks out over the surrounding high-rises toward the coastline, making it a highly desirable beachside retreat.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 2, Baths = 3, Parking = 10, LandSize = 3600, Price = 205000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("del_perro_heights_apt_20"),
                    Latitude = 34.0192694, Longitude = -118.4981403,
                },
                new Property
                {
                    Id = "property_29", Name = "1561 San Vitas Street, Apt. 2",
                    Address = "5959 Franklin Avenue, Los Angeles, CA 90028",
                    Description = "A highly customized medium-end apartment inside a landmark 1920s Spanish Colonial Revival complex. The interior features restored hardwood flooring, exposed ceiling beams, and arched doorways. Positioned on a lower residential tier, its windows overlook a lush, Mediterranean-style central courtyard fountain and the local palm-lined boulevard.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.COMMON,
                    Beds = 2, Baths = 1, Parking = 2, LandSize = 1800, Price = 99000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("san_vitas_street_1561_apt_2"),
                    Latitude = 34.1053367, Longitude = -118.3196482,
                },
                new Property
                {
                    Id = "property_30", Name = "4 Integrity Way, Apt. 35",
                    Address = "1000 Wilshire Boulevard, Los Angeles, CA 90017",
                    Description = "A premier corporate-chic suite finished in polished dark granite and chrome accents. Perched on a high floor, its floor-to-ceiling windows look straight out over the intersecting downtown freeway loops and the glowing structural grids of the financial district.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.COMMON,
                    Beds = 2, Baths = 2, Parking = 10, LandSize = 3800, Price = 247000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("integrity_way_4_apt_35"),
                    Latitude = 34.051474, Longitude = -118.261657,
                },
                new Property
                {
                    Id = "property_31", Name = "4 Integrity Way, Apt. 30",
                    Address = "1000 Wilshire Boulevard, Los Angeles, CA 90017",
                    Description = "A sleek, fully customized apartment featuring floating accent walls, embedded LED lighting arrays, and a premium built-in media lounge. Positioned on a mid-level tier, it offers a dense, immersive urban perspective of surrounding corporate high-rises.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.COMMON,
                    Beds = 2, Baths = 2, Parking = 10, LandSize = 3500, Price = 235000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("integrity_way_4_apt_30"),
                    Latitude = 34.051474, Longitude = -118.261657,
                },
                new Property
                {
                    Id = "property_32", Name = "3 Alta Street Tower, Apt 57",
                    Address = "555 West 5th Street, Los Angeles, CA 90013",
                    Description = "An ultra-premium executive sky loft featuring a dramatic floating steel staircase and double-height glass panels. Sits on a dominant high tier, offering vast views looking down onto the corporate plaza below and across the entire downtown Los Angeles skyline.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.PREMIUM,
                    Beds = 3, Baths = 3, Parking = 10, LandSize = 4200, Price = 223000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("alta_street_3_tower_apt_57"),
                    Latitude = 34.0500274, Longitude = -118.2533141,
                },
                new Property
                {
                    Id = "property_33", Name = "3 Alta Street Tower, Apt 10",
                    Address = "555 West 5th Street, Los Angeles, CA 90013",
                    Description = "A customized contemporary flat styled with textured concrete and minimalist industrial elements. Located on a lower residential tier of the skyscraper, its glass facade overlooks the kinetic energy of the street-level business plazas and towering adjacent high-rises.",
                    Type = PropertyType.APARTMENT, Tier = PropertyTier.COMMON,
                    Beds = 2, Baths = 2, Parking = 10, LandSize = 3400, Price = 217000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("alta_street_3_tower_apt_10"),
                    Latitude = 34.0500274, Longitude = -118.2533141,
                },
                new Property
                {
                    Id = "property_34", Name = "The Chumash Bunker",
                    Address = "Chatsworth Park North, Chatsworth, Los Angeles, CA 91311",
                    Description = "A highly secure, concrete-reinforced underground military fortress drilled directly into the coastal cliffside. The fully upgraded variant features active machinery grids, humming ventilation arrays, and heavy forklift paths moving weapon crates, offering complete radar anonymity and absolute defense against structural damage.",
                    Type = PropertyType.BUNKER, Tier = PropertyTier.TACTICAL,
                    Beds = 1, Baths = 2, Parking = 2, LandSize = 45000, Price = 1650000, AgentId = "agent_maze_bank_foreclosures",
                    ImageUrls = GetPropertyImageUrls("the_chumash_bunker"),
                    Latitude = 34.2583, Longitude = -118.5914,
                },
                new Property
                {
                    Id = "property_35", Name = "Grand Senora Desert Bunker",
                    Address = "100 Jackson Drive, Edwards AFB, CA 93524",
                    Description = "A completely hidden, high-security industrial bunker buried beneath the desert sand. Positioned adjacent to local runway grids, this upgraded facility is designed for quick weapon transit, relying on massive concrete blast shields and a completely self-contained power grid to run automated ammunition production lines.",
                    Type = PropertyType.BUNKER,
                    Beds = 1, Baths = 2, Parking = 3, LandSize = 45000, Price = 2120000, AgentId = "agent_maze_bank_foreclosures",
                    ImageUrls = GetPropertyImageUrls("grand_senora_desert_bunker"),
                    Latitude = 34.1201243, Longitude = -117.4649267,
                },
                new Property
                {
                    Id = "property_36", Name = "3655 Wild Oats Drive",
                    Address = "2220 Avenue of the Stars, Century City, Los Angeles, CA 90067",
                    Description = "An ultra-premium residential condo inside an iconic twin-tower mid-century modern complex. Fully customized with polished concrete flooring, recessed gallery track lighting, and a professional culinary kitchen. Its floor-to-ceiling windows look straight west across the rolling lawns of private country clubs and the distant ocean horizon.",
                    Type = PropertyType.HOUSE, Tier = PropertyTier.TACTICAL,
                    Beds = 2, Baths = 2, Parking = 10, LandSize = 3800, Price = 800000, AgentId = "agent_dynasty_8",
                    ImageUrls = GetPropertyImageUrls("wild_oats_drive_3655"),
                    Latitude = 34.0526055, Longitude = -118.4096627,
                },
                new Property
                {
                    Id = "property_37", Name = "Downtown Vinewood Clubhouse",
                    Address = "1340 East 6th Street, Los Angeles, CA 90021",
                    Description = "A heavily fortified, industrial brick warehouse converted into a premium motorcycle headquarters. The fully upgraded variant features exposed masonry walls, dark leather upholstery, steel ceiling trusses, and concrete floors built to handle constant vehicle foot traffic.",
                    Type = PropertyType.CLUBHOUSE, Tier = PropertyTier.COMMERCIAL,
                    Beds = 1, Baths = 2, Parking = 17, LandSize = 8500, Price = 472000, AgentId = "agent_maze_bank_foreclosures",
                    ImageUrls = GetPropertyImageUrls("downtown_vinewood_clubhouse"),
                    Latitude = 34.038143, Longitude = -118.234319,
                },
                new Property
                {
                    Id = "property_38", Name = "Del Perro Nightclub",
                    Address = "225 Santa Monica Boulevard, Santa Monica, CA 90401",
                    Description = "A world-class underground dance club hidden behind a historic Art Deco facade. The fully customized variant features premium sound systems, an array of multi-colored overhead lasers, dry ice blasters, and a massive sub-level contraband warehouse operating around the clock.",
                    Type = PropertyType.NIGHTCLUB, Tier = PropertyTier.PREMIUM,
                    Beds = 1, Baths = 1, Parking = 39, LandSize = 32000, Price = 455000, AgentId = "agent_maze_bank_foreclosures",
                    ImageUrls = GetPropertyImageUrls("del_perro_nightclub"),
                    Latitude = 34.0155258, Longitude = -118.4967136,
                },
                new Property
                {
                    Id = "property_39", Name = "Downtown Vinewood Nightclub",
                    Address = "1735 Vine Street, Los Angeles, CA 90028",
                    Description = "A premier electronic music sanctuary housed in a landmark Hollywood entertainment venue. The fully upgraded interior boasts deep velvet textures, structural iron pillars, industrial neon accent loops, and highly secure lower staging zones for major distribution lines.",
                    Type = PropertyType.NIGHTCLUB, Tier = PropertyTier.PREMIUM,
                    Beds = 1, Baths = 1, Parking = 39, LandSize = 32000, Price = 1670000, AgentId = "agent_maze_bank_foreclosures",
                    ImageUrls = GetPropertyImageUrls("downtown_vinewood_nightclub"),
                    Latitude = 34.1027177, Longitude = -118.326997,
                },
                new Property
                {
                    Id = "property_40", Name = "Videogeddon Arcade",
                    Address = "2321 East 8th Street, Los Angeles, CA 90021",
                    Description = "A vibrant, neon-soaked retro arcade that serves as a front for a massive underground operations hub. The fully customized layout features pixel art wall murals, flashing cabinet lights, a private office glass view, and a concrete-reinforced tactical basement engineered to execute high-stakes casino infiltrations.",
                    Type = PropertyType.ARCADE, Tier = PropertyTier.COMMERCIAL,
                    Beds = 0, Baths = 2, Parking = 10, LandSize = 12500, Price = 1875000, AgentId = "agent_maze_bank_foreclosures",
                    ImageUrls = GetPropertyImageUrls("videogeddon_arcade"),
                    Latitude = 34.029675, Longitude = -118.2314437,
                },
                new Property
                {
                    Id = "property_41", Name = "Mission Row Auto Shop ",
                    Address = "432 Southeast 6th Street, Los Angeles, CA 90014",
                    Description = "A premium, fully customized tuner sanctuary housed in a gritty brick industrial storefront. The interior features polished epoxy flooring, industrial metal steel beams, custom graffiti wall murals, and high-intensity overhead LED track lighting designed to showcase high-end street racing builds",
                    Type = PropertyType.AUTOSHOP, Tier = PropertyTier.COMMERCIAL,
                    Beds = 1, Baths = 1, Parking = 12, LandSize = 9500, Price = 1670000, AgentId = "agent_maze_bank_foreclosures",
                    ImageUrls = GetPropertyImageUrls("mission_row_auto_shop"),
                    Latitude = 34.042602, Longitude = -118.245959,
                },
                new Property
                {
                    Id = "property_42", Name = "Burton Auto Shop",
                    Address = "8500 Santa Monica Boulevard, West Hollywood, CA 90069",
                    Description = "A sleek, hyper-modern automotive tuning shop featuring clean minimalist steel panels, exposed ventilation trunks, and bright white workspace lighting. Perfectly suited for an elite player profile managing high-tier underground contracts.",
                    Type = PropertyType.AUTOSHOP, Tier = PropertyTier.COMMERCIAL,
                    Beds = 1, Baths = 1, Parking = 12, LandSize = 9500, Price = 1830000, AgentId = "agent_maze_bank_foreclosures",
                    ImageUrls = GetPropertyImageUrls("burton_auto_shop"),
                    Latitude = 34.0889942, Longitude = -118.3766725,
                }
            };
        }

        private void LoadAgents()
        {
            _agents = new List<Agent>
            {
                new Agent
                {
                    Id = "agent_maze_bank_foreclosures",
                    Email = "support@mazebankforeclosures.com",
                    Name = "Maze Bank Foreclosures",
                    Phone = "(310) 555-0198",
                    Description = "Maze Bank Foreclosures specializes in repossessed and distressed commercial properties across Los Santos and Blaine County. From abandoned businesses to lucrative criminal enterprises, our listings offer ambitious buyers the opportunity to acquire valuable properties at competitive prices.",
                    Website = "https://www.maze-bank.com/foreclosures",
                    Specialization = "Foreclosures & Commercial Properties",
                    AgentType = "Bank-Owned Real Estate",
                    OpeningHours = "Mon-Fri: 08:00-18:00",
                    ImageUrl = $"{GlobalSettings.Instance.ImageBaseUrl}agent_maze_bank_foreclosures.jpg"
                },
                new Agent
                {
                    Id = "agent_dynasty_8",
                    Email = "sales@dynasty8.com",
                    Name = "Dynasty 8",
                    Phone = "(310) 555-0148",
                    Description = "Dynasty 8 is Los Santos' premier destination for residential real estate. Whether you're looking for a stylish apartment in the city or a comfortable home in the suburbs, our extensive portfolio has something for every prospective homeowner.",
                    Website = "https://www.dynasty8.com",
                    Specialization = "Residential Properties",
                    AgentType = "Real Estate Agency",
                    OpeningHours = "Mon-Sat: 09:00-19:00",
                    ImageUrl = $"{GlobalSettings.Instance.ImageBaseUrl}agent_dynasty_8.jpg"
                },
                new Agent
                {
                    Id = "agent_dynasty_8_executive",
                    Email = "executive@dynasty8.com",
                    Name = "Dynasty 8 Executive",
                    Phone = "(310) 555-0166",
                    Description = "Dynasty 8 Executive provides premium commercial real estate services for Los Santos' most ambitious entrepreneurs. Our exclusive portfolio includes high-end office spaces and executive properties designed for businesses ready to operate at the highest level.",
                    Website = "https://www.dynasty8.com/executive",
                    Specialization = "Executive Offices & Commercial Properties",
                    AgentType = "Premium Real Estate Agency",
                    OpeningHours = "Mon-Fri: 08:00-20:00",
                    ImageUrl = $"{GlobalSettings.Instance.ImageBaseUrl}agent_dynasty_8_executive.jpg"
                },
                new Agent
                {
                    Id = "agent_diamond_casino_resort",
                    Email = "vip@thediamondcasino.com",
                    Name = "Diamond Casino & Resort",
                    Phone = "(310) 555-0888",
                    Description = "The Diamond Casino & Resort offers an exclusive residential experience for Los Santos' most distinguished guests. The Diamond Casino Penthouse provides luxury living, private entertainment and unparalleled access to the finest amenities in the city.",
                    Website = "https://www.thediamondcasino.com",
                    Specialization = "Luxury Penthouse",
                    AgentType = "Casino & Resort",
                    OpeningHours = "Open 24 Hours",
                    ImageUrl = $"{GlobalSettings.Instance.ImageBaseUrl}agent_diamond_casino_resort.jpg"
                },
                new Agent
                {
                    Id = "agent_prix_luxury_real_estate",
                    Email = "concierge@prixluxury.com",
                    Name = "Prix Luxury Real Estate",
                    Phone = "(310) 555-0777",
                    Description = "Prix Luxury Real Estate specializes in exceptional residences for clients who expect nothing less than the best. Our exclusive properties combine breathtaking architecture, premium amenities and unrivalled privacy throughout Los Santos and the surrounding areas.",
                    Website = "https://www.prixluxury.com",
                    Specialization = "Luxury Mansions & Estates",
                    AgentType = "Luxury Real Estate",
                    OpeningHours = "Mon-Sun: 09:00-21:00",
                    ImageUrl = $"{GlobalSettings.Instance.ImageBaseUrl}agent_prix_luxury_real_estate.jpg"
                }
            };
        }

        private List<string> GetPropertyImageUrls(string propertyName)
        {
            var mainImageName = $"{propertyName}.jpg";

            return GeneratedPropertyImages.All
                .Where(image =>
                    image.Equals(mainImageName, StringComparison.OrdinalIgnoreCase) ||
                    image.StartsWith($"{propertyName}_", StringComparison.OrdinalIgnoreCase))
                .OrderBy(image =>
                    image.Equals(mainImageName, StringComparison.OrdinalIgnoreCase) ? 0 : 1)
                .ToList();
        }

    }
}

