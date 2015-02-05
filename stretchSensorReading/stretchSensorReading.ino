
const int numReadings = 10;

int readings[numReadings];
int index = 0;
int total = 0;
int average = 0;

int sensorPin = A1;
int sensorValue;

void setup(){
  Serial.begin(9600);
  for (int thisReading = 0; thisReading < numReadings; thisReading++)
    readings[thisReading] = 0;
}

void loop(){
    total = total - readings[index];
    readings[index] = analogRead(sensorPin);
    total = total + readings[index];
    index = index + 1;
    if( index >= numReadings)
      index = 0;
    
    sensorValue = total / numReadings;
    
    Serial.println(sensorValue);
    Serial.flush();
    delay(30);
}
