
const int numReadings = 10;

int readings[numReadings];
int readings2[numReadings];

int index = 0;
int total = 0;
int average = 0;

int index2 = 0;
int total2 = 0;
int average2 = 0;

int sensorPin01 = A1;
int sensorPin02 = A2;
int sensorValue, sensorValue2;

void setup(){
  Serial.begin(9600);
  for (int thisReading = 0; thisReading < numReadings; thisReading++)
    readings[thisReading] = 0;
  for (int thisReading = 0; thisReading < numReadings; thisReading++)
    readings2[thisReading] = 0;
}

void loop(){
    total = total - readings[index];
    readings[index] = analogRead(sensorPin01);
    total = total + readings[index];
    index = index + 1;
    if( index >= numReadings)
      index = 0;
    
    total2 = total2 - readings2[index2];
    readings2[index2] = analogRead(sensorPin02);
    total2 = total2 + readings2[index2];
    index2 = index2 + 1;
    if( index2 >= numReadings)
      index2 = 0;
    
    sensorValue = total / numReadings;
    
    sensorValue2 = total2 / numReadings;
    
    Serial.println("B");
    Serial.println(sensorValue);
    
    Serial.print("A");
    Serial.println(sensorValue2);
    
    Serial.flush();
    delay(30);
}
