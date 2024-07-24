namespace Task_2_Books.Interfaces;



public interface IMapper<TSource, TDestination>
{
        TDestination Map(TSource source);
}
