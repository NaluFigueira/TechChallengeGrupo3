using System;

namespace PosTech.TechChallenge.Contacts.Command.Infra.Interfaces;

public interface IProducer
{
    void PublishMessageOnQueue<T>(T messageBody, string queueName);
}
