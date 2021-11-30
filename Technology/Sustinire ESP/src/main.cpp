#include <Arduino.h>
#include <OneWire.h>
#include <DallasTemperature.h>

#define ONE_WIRE_BUS 23

OneWire oneWire(ONE_WIRE_BUS);

DallasTemperature sensors(&oneWire);

double rtTemp = 0;
double gemTemp = 0;
int tempTick = 0;
double aantalJoule = 0;
double gasVerbruik = 0;
double gemTempNa = 0;

const int nColors = 3; //Aantal kleuren
const int colorPin[nColors] = {13, 12, 14};

int red = 0;
int green = 1;
int blue = 2;

int pauze = 500;

String messageInput;

int sensorInterrupt = 0;
int sensorPin = 25;
int solenoidValve = 5;

unsigned int SetPoint = 400;

float calibrationFactor = 90;

volatile byte pulseCount = 0;

float flowRate = 0.0;
unsigned int flowMilliLitres = 0;
unsigned long totalMilliLitres = 0;

unsigned long oldTime = 0;

void ledOn(int color, int intensity)
{
  digitalWrite(colorPin[color], intensity);
}

void ledOff(int color)
{
  digitalWrite(colorPin[color], 0);
}

void allLedOff()
{
  for (int i = 0; i < nColors; i++)
  {
    ledOff(i);
  }
}

void colorMix(int redI, int greenI, int blueI)
{
  ledOn(red, redI);
  ledOn(green, greenI);
  ledOn(blue, blueI);
}

void pulseCounter()
{
  pulseCount++;
}

void SetSolinoidValve()
{
  digitalWrite(solenoidValve, LOW);
}

void setup()
{
  Serial.begin(115200);
  sensors.begin();

  pinMode(solenoidValve, OUTPUT);
  digitalWrite(solenoidValve, HIGH);
  pinMode(sensorPin, INPUT);
  digitalWrite(sensorPin, HIGH);

  attachInterrupt(digitalPinToInterrupt(sensorPin), pulseCounter, FALLING);

  for (int i = 0; i < nColors; i++)
  {
    pinMode(colorPin[i], OUTPUT);
  }
}

void loop()
{

  if ((millis() - oldTime) > 1000)
  {

    detachInterrupt(sensorInterrupt);

    flowRate = ((1000.0 / (millis() - oldTime)) * pulseCount) / calibrationFactor;

    oldTime = millis();

    flowMilliLitres = (flowRate / 60) * 1000;

    totalMilliLitres += flowMilliLitres;

    unsigned int frac;

    if (totalMilliLitres > 40)
    {
      SetSolinoidValve();
    }

    pulseCount = 0;

    attachInterrupt(sensorInterrupt, pulseCounter, FALLING);
  }

  if (totalMilliLitres > 0 && totalMilliLitres < 25)
  {
    colorMix(0, 255, 0); //green
  }

  if (totalMilliLitres > 25 && totalMilliLitres < 75)
  {
    colorMix(0, 0, 255); //blue
  }

  if (totalMilliLitres > 75)
  {
    colorMix(255, 0, 0); //red
  }

  sensors.requestTemperatures();

  if (flowMilliLitres > 0)
  {
    rtTemp += sensors.getTempCByIndex(0);
    tempTick += 1;
    delay(1000);
  }

  else
  {
    if (rtTemp > 0 && tempTick > 0)
    {

      gemTemp = rtTemp / tempTick;

      gemTempNa = gemTemp - 13.5;

      aantalJoule += totalMilliLitres * 0.997 * 4176 * gemTempNa; //Q = m * c * ΔT

      gasVerbruik += aantalJoule / 31650000;
      Serial.print(gasVerbruik);  
      Serial.println(" m3");            //m3
      Serial.print(totalMilliLitres);
      Serial.println(" mL"); //mL

      rtTemp = 0;
      tempTick = 0;
      totalMilliLitres = 0;

      allLedOff();
    }
  }
}