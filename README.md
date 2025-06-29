# SmartBahn

SmartBahn ist eine C#-Desktopanwendung, mit der Nutzer Berliner ÖPNV-Haltestellen suchen und aktuelle Abfahrtszeiten in Echtzeit abrufen können. Die Daten werden über die öffentliche BVG-API bereitgestellt. Das Projekt wurde im Rahmen einer Lehrveranstaltung zur Softwareentwicklung und objektorientierten Programmierung umgesetzt.

# Funktionen

- Haltestellen-Suche nach Namen mit Anzeige der Live-Abfahrten
- Anzeige von Verspätungen durch Vergleich von Soll- und Ist-Zeiten
- Einfache statistische Auswertung (z. B. durchschnittliche Verspätung, Verspätungsquote)
- Benutzerfreundliche Oberfläche
- (Optional) Prognose von Verspätungen mit einem trainierten ML.NET-Modell

# Verwendete Technologien

- C# (.NET 6 oder höher)
- BVG Transport REST API
- WinForms oder WPF für die Benutzeroberfläche
- LiveCharts2 oder ScottPlot zur Datenvisualisierung
- (Optional) ML.NET für maschinelles Lernen und Prognosen
