﻿Наследование (треугольная не закрашен)
Наследование является базовым принципом ООП и позволяет одному классу (наследнику) унаследовать функционал другого класса (р

Реализация (треугольная не закрашен пунктир) - интерфейсы
Реализация предполагает определение интерфейса и его реализация в классаходительского)

Ассоциация (стрела * 1)
Ассоциация - это отношение, при котором объекты одного типа неким образом связаны с объектами другого типа

Композиция (стрела закрашенный ромбик)
Композиция определяет отношение HAS A, то есть отношение "имеет"

Агрегация (стрела не закрашенный ромбик)
От композиции следует отличать агрегацию. Она также предполагает отношение HAS A, но реализуется она иначе
public abstract class Engine
{ }

public class Car
{
    Engine engine;
    public Car(Engine eng)
    {
        engine = eng;
    }
}
При агрегации реализуется слабая связь, то есть в данном случае объекты Car и Engine будут равноправны. 
В конструктор Car передается ссылка на уже имеющийся объект Engine. И, как правило, определяется ссылка не 
на конкретный класс, а на абстрактный класс или интерфейс, что увеличивает гибкость программы