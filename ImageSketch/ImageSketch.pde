PImage _image;

void setup()
{
  _image = loadImage("vantan.jpg");
  surface.setSize(_image.width,_image.height);
  /*
  _image.filter(BLUR,3);
  */
  
  float sum;
  
  for(int x = 1; x < _image.width - 1; x++)
  {
    for(int y = 1; y < _image.height - 1;y++)
    {
      sum = _image.get(x-1,y-1) / 16 + _image.get(x  ,y-1) / 16 * 2 + _image.get(x+1,y-1) / 16 
          + _image.get(x-1,y  ) / 16 * 2 + _image.get(x  ,y  ) / 16 * 4 + _image.get(x+1,y  ) / 16 * 2 
          + _image.get(x-1,y+1) / 16 + _image.get(x  ,y+1) / 16 * 2 + _image.get(x+1,y+1) / 16; 
      _image.set(x, y, color(sum));
    }
  }
}

void draw()
{
  image(_image,0,0);
}
