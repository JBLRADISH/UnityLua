public class LuaBinder
{
	public static void Register()
	{
		ObjectWrap.Register();
		GameObjectWrap.Register();
		ComponentWrap.Register();
		TransformWrap.Register();
	}
}
