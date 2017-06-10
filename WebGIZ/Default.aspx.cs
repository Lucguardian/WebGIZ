using System;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Default : System.Web.UI.Page
{
	private SharpMap.Map myMap;

	protected void Page_Load(object sender, EventArgs e)
	{
		//Set up the map
		myMap = InitializeMap(new System.Drawing.Size((int)imgMap.Width.Value,(int)imgMap.Height.Value));
		if (Page.IsPostBack) 
		{
			//Page is post back. Restore center and zoom-values from viewstate
			myMap.Center = (SharpMap.Geometries.Point)ViewState["mapCenter"];
			myMap.Zoom = (double)ViewState["mapZoom"];
		}
		else
		{
			//This is the initial view of the map. Zoom to the extents of the map:
			myMap.ZoomToExtents();
			//Save the current mapcenter and zoom in the viewstate
			ViewState.Add("mapCenter", myMap.Center);
			ViewState.Add("mapZoom", myMap.Zoom);
			//Create the map
			CreateMap();
		}
	}
  
	protected void imgMap_Click(object sender, ImageClickEventArgs e)
	{
		//Set center of the map to where the client clicked
		myMap.Center = SharpMap.Utilities.Transform.MapToWorld(new System.Drawing.Point(e.X, e.Y), myMap);
		//Set zoom value if any of the zoom tools were selected
		if (rblMapTools.SelectedValue == "0") //Zoom in
			myMap.Zoom = myMap.Zoom * 0.5;
		else if (rblMapTools.SelectedValue == "1") //Zoom out
			myMap.Zoom = myMap.Zoom * 2;
		//Save the new map's zoom and center in the viewstate
		ViewState.Add("mapCenter", myMap.Center);
		ViewState.Add("mapZoom", myMap.Zoom);
		//Create the map
		CreateMap();
	}
  
	/// <summary>
	/// Sets up the map, add layers and sets styles
	/// </summary>
	/// <param name="outputsize">Initiatial size of output image</param>
	/// <returns>Map object</returns>
	private SharpMap.Map InitializeMap(System.Drawing.Size outputsize)
	{
		//Initialize a new map of size 'imagesize'
		SharpMap.Map map = new SharpMap.Map(outputsize);
				
		//Set up the countries layer
		SharpMap.Layers.VectorLayer layCountries = new SharpMap.Layers.VectorLayer("Countries");
		//Set the datasource to a shapefile in the App_data folder
		layCountries.DataSource = new SharpMap.Providers.ShapeFile(Server.MapPath(@"~\App_data\countries.shp"), true);
		//Set fill-style to green
		layCountries.Style.Fill = new SolidBrush(Color.Green);
		//Set the polygons to have a black outline
		layCountries.Style.Outline = System.Drawing.Pens.Black;
		layCountries.Style.EnableOutline = true;

		//Set up a river layer
		SharpMap.Layers.VectorLayer layRivers = new SharpMap.Layers.VectorLayer("Rivers");
		//Set the datasource to a shapefile in the App_data folder
		layRivers.DataSource = new SharpMap.Providers.ShapeFile(Server.MapPath(@"~\App_data\rivers.shp"), true);
		//Define a blue 1px wide pen
		layRivers.Style.Line = new Pen(Color.Blue,1);

		//Add the layers to the map object.
		//The order we add them in are the order they are drawn, so we add the rivers last to put them on top
		map.Layers.Add(layCountries);
		map.Layers.Add(layRivers);
		
		return map;
	}

	/// <summary>
	/// Creates the map, inserts it into the cache and sets the ImageButton Url
	/// </summary>
	private void CreateMap()
	{
		System.Drawing.Image img = myMap.GetMap();
		string imgID = SharpMap.Web.Caching.InsertIntoCache(1, img);
		imgMap.ImageUrl = "getmap.aspx?ID=" + HttpUtility.UrlEncode(imgID);
	}
}
