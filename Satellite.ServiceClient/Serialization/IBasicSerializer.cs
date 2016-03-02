namespace Satellite.ServiceClient.Serialization
{
	public interface IBasicSerializer<T>
	{
		T DeSerialize(string data);
		string Serialze(T data);
	}
}