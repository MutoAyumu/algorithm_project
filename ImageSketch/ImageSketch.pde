PImage _image;

void setup()
{
  _image = loadImage("vantan.jpg");
  surface.setSize(_image.width,_image.height);
  //_image.resize(_image.width * 10, _image.height * 10);
}

void draw()
{
  image(_image,0,0);
}
