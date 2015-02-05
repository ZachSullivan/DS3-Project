
Flock flock;

void setup() {
  size( 1024 , 768 );
  flock = new Flock();
  // Add an initial set of boids into the system
<<<<<<< Updated upstream
  for (int i = 0; i < 500; i++) {
    flock.addBoid(new Boid(width/2,height/2));
=======
  for (int i = 0; i < 100; i++) {
    if( i % 2 == 0 )
      flock.addBoid(new Boid(width/2,height/2, 0));
    else
      flock.addBoid(new Boid(width/2,height/2, 1));
>>>>>>> Stashed changes
  }
}

void draw() {
  background(50);
  flock.run();
}

// Add a new boid into the System
<<<<<<< Updated upstream
//Removed ability to spawn more then the starting number of boids
/*void mousePressed() {
  flock.addBoid(new Boid(mouseX,mouseY));
}*/
=======
void mousePressed() {
  flock.addBoid(new Boid(mouseX,mouseY,1));
}
>>>>>>> Stashed changes
