
const int numReadings = 10;

int readings[numReadings];
int readings2[numReadings];
int readings3[numReadings];

int index = 0;
int total = 0;
int average = 0;

int index2 = 0;
int total2 = 0;
int average2 = 0;

int index3 = 0;
int total3 = 0;
int average3 = 0;

int sensorPin01 = A1;
int sensorPin02 = A2;
int sensorPin03 = A3;

int sensorValue, sensorValue2, sensorValue3;

void setup(){
  Serial.begin(9600);
  for (int thisReading = 0; thisReading < numReadings; thisReading++)
    readings[thisReading] = 0;
  for (int thisReading = 0; thisReading < numReadings; thisReading++)
    readings2[thisReading] = 0;
  for (int thisReading = 0; thisReading < numReadings; thisReading++)
    readings3[thisReading] = 0;
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
    //total2 = readings2[index2];
    total2 = total2 + readings2[index2];
    index2 = index2 + 1;
    if( index2 >= numReadings)
      index2 = 0;
      
    total3 = total3 - readings3[index3];
    readings3[index3] = analogRead(sensorPin03);
    total3 = total3 + readings3[index3];
    index3 = index3 + 1;
    if( index3 >= numReadings)
      index3 = 0;
      
      
    sensorValue = total / numReadings;
    
    sensorValue2 = total2 / numReadings;
    
    sensorValue3 = total3 / numReadings; 
    
    int val1 = (int) map( sensorValue, 350, 600, 0, 15);
    int val2 = (int) map( sensorValue2, 300, 400, 0, 15);
    int val3 = (int) map( sensorValue3, 150, 300, 0, 15);
    /**
    Serial.println( val1 );
    Serial.println( val2 );
    Serial.println( val3 );
    **/
    int combinedVal = val1 | ( val2 << 4 ) | ( val3 << 8 );
    
    Serial.println( combinedVal );
    
    /**
    int remapVal1 = combinedVal & 0xF;
    int remapVal2 = (combinedVal >> 4) & 0xF;
    int remapVal3 = (combinedVal >> 8) & 0xF;
    **/
    
    
    /**
    Serial.println( remapVal1 );
    Serial.println( remapVal2 );
    Serial.println( remapVal3 );
    Serial.println( " " );
    **/
    /**
    Serial.println("B");
    Serial.println(sensorValue);
    
    Serial.print("A");
    Serial.println(sensorValue2);
    **/
    Serial.flush();
    delay(30);
}
