using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Paint : MonoBehaviour {
	[Range(1,128)]
	public int textureWidth = 20; 
	[Range(1,128)]
	public int textureHeight = 20;
	public Color foreColor = Color.green;
	public Color backColor = Color.yellow;
	Texture2D _texture;
	private int[] _gird = new int[100] ;
	public string ledString = "" ; 
    public char currentDisplayMode = 'a' ;

	// Use this for initialization
	
	void Start () {
		Debug.developerConsoleVisible = true;
		CreateTexture();
		Fill(backColor);
	}
	void CreateTexture(){
		_texture = new Texture2D(textureWidth,textureHeight);
		_texture.filterMode = FilterMode.Point;
		this.gameObject.GetComponent<MeshRenderer>().material.SetTexture("_MainTex",_texture);
		
	}

	void Update () {
		if(Input.GetKey(KeyCode.Mouse0)) // left button of mouse
		{
			WhileMousePressed();
		}
		else if(Input.GetKeyDown(KeyCode.Space)) // for testing purpose
		{
			PixelPrinter();
		}
	}
	void Fill(Color color)
	{
		Color[] pixels= _texture.GetPixels();
		for (int i = 0;i<pixels.Length;i++)
		{
			pixels[i] = color;
		}
		_texture.SetPixels(pixels);
		_texture.Apply();
	}

    // Update is called once per frame
 
	void WhileMousePressed()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if(Physics.Raycast(ray,out hitInfo))
		{
			var pixelCoords = uv2Pixel(hitInfo.textureCoord);
			Stroke(pixelCoords,foreColor);
		}

	}
	void Stroke(Vector2Int pixelsCoords, Color color)
	{
		_texture.SetPixel(pixelsCoords.x,pixelsCoords.y,color);
		int tmp = pixelsCoords.x+pixelsCoords.y*10; // Calculate the coordinate 
		switch (foreColor)
		{
			case Color.black: 
				_gird[tmp]=0;
				break;
			case Color.red:
				_gird[tmp]=1;
				break;
			case Color.blue:
				_gird[tmp]=2;
				break;
			case Color.green:
				_gird[tmp]=3;
				break;
			case Color.white:
				_gird[tmp]=4;
				break;
			case Color.yellow:
				_gird[tmp]=5;
				break;
			case Color.magenta:
				_gird[tmp]=6;
				break;
			case Color.cyan:
				_gird[tmp]=7;
				break;
			default:
				Console.log("The color is unknown.");
				break;
		}


		_texture.Apply();

	}
	Vector2Int uv2Pixel(Vector2 uv)
	{
		int x  = Mathf.FloorToInt(uv.x*textureWidth);
		int y  = Mathf.FloorToInt(uv.y*textureHeight);
		return new Vector2Int(x,y);
	}

	
	public void PixelPrinter()
	{
		ledString = "";
		for(int i  =0 ; i<100 ; i++)
		{
			ledString +=_gird[i];
		}
		
	}
 
    public void CleanAll() // Remove every pixel on the texture
    {
        for (int i = 0; i < 100; i++)
        {
            _gird[i] = 0;
        }
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                _texture.SetPixel(x, y, Color.black); // set everything into black
            }

        }
        _texture.Apply();
        
    }
    public void DisplayMode1() // Set the layout of the pattern 
    {
        currentDisplayMode = 'a';
    }
    public void DisplayMode2()
    {
        currentDisplayMode = 'b';
    }
    public void DisplayMode3()
    {
        currentDisplayMode = 'c';
    }
    public void DisplayMode4()
    {
        currentDisplayMode = 'd';
    }
    public void SelectRed()
	{
		foreColor = Color.red;
	}
	public void SelectBlue()
	{
		foreColor = Color.blue;
	}
	public void SelectGreen()
	{
		foreColor = Color.green;
		
	}
	public void SelectWhite()
	{
		foreColor = Color.white;
	}
	public void SelectBlack() //Use this as eraser, No color
	{
		foreColor = Color.black;
	}
	public void SelectYellow()
	{
		foreColor = Color.yellow;
	}
	public void SelectPurple()
	{
		foreColor = Color.magenta; //Use Magenta as purple
	}
	public void SelectLightBlue()
	{
		foreColor = Color.cyan; //Use cyan as lightblue
	}


}
