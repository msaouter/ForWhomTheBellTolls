
const int pinBellOne = 2;  //Pin Reed
const int pinBellTwo = 4;  //Pin Reed
const int pinBellThree = 7;  //Pin Reed
const int pinBellFour = 8;  //Pin Reed
const int pinBellFive = 12;  //Pin Reed
const int pinBellSix = 13;  //Pin Reed

//const int pinLed    = 9;  //Pin LED
int StateSwitch1 = 0;
int StateSwitch2 = 0;
int StateSwitch3 = 0;
int StateSwitch4 = 0;
int StateSwitch5 = 0;
int StateSwitch6 = 0;

void setup()
{
  //pinMode(LED_BUILTIN, OUTPUT);
  pinMode(pinBellOne, INPUT);
  pinMode(pinBellTwo, INPUT);
  pinMode(pinBellThree, INPUT);
  pinMode(pinBellFour, INPUT);
  pinMode(pinBellFive, INPUT);
  pinMode(pinBellSix, INPUT);

  //Debug
  //digitalWrite(13, LOW);
  
  //Open the communication with Unity
  Serial.begin(9600);
}
void loop()
{
  //save the state of every magnetic reed
  StateSwitch1 = digitalRead(pinBellOne);
  StateSwitch2 = digitalRead(pinBellTwo);
  StateSwitch3 = digitalRead(pinBellThree);
  StateSwitch4 = digitalRead(pinBellFour);
  StateSwitch5 = digitalRead(pinBellFive);
  StateSwitch6 = digitalRead(pinBellSix);

  //creation of the string with state of every button
  // String to parse at every ","
  Serial.print(StateSwitch1);
  Serial.print(",");
  Serial.print(StateSwitch2);
  Serial.print(",");
  Serial.print(StateSwitch3);
  Serial.print(",");
  Serial.print(StateSwitch4);
  Serial.print(",");
  Serial.print(StateSwitch5);
  Serial.print(",");
  Serial.println(StateSwitch6);
  
  //HIGH : le courant passe pas
  // LOW : le courant passe
  //DEBUG
  /*if(StateSwitch1 == HIGH){
    digitalWrite
    (13, LOW);
  }
  else{
    digitalWrite(13, HIGH);
  }*/
  
}//+++++++++++++++
