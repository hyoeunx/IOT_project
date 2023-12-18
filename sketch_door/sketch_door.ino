#include<Servo.h>

Servo servo;

#define trig 7
#define echo 6
int angle = 0;


void setup() {
  servo.attach(2);
  Serial.begin(9600);
  pinMode(trig, OUTPUT);
  pinMode(echo, INPUT);
}

void loop(){
  long duration, distance;

  digitalWrite(trig, LOW);
  digitalWrite(echo, LOW);
  delayMicroseconds(2);
  digitalWrite(trig, HIGH);
  delayMicroseconds(10);
  digitalWrite(trig, LOW);

  duration = pulseIn(echo, HIGH);
  distance = ((float)(340 * duration) / 10000) / 2;

  Serial.print("거리 : ");
  Serial.print(distance);
  Serial.println("cm");

  if(distance < 10) {
    angle = 90;
    Serial.println("문이 열렸습니다.");
    delay(500);
  }
  else {
    angle = 180;
    delay(500);
  }
  servo.write(angle);
}
