using tsdn.Dependency;

namespace tsdn.Service.Contract
{
    public interface IDogService: ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string SayName();

        /// <summary>
        /// 吃狗粮
        /// </summary>
        /// <param name="dogFood">重量</param>
        /// <returns></returns>
        string Eat(double dogFood);
    }
}
