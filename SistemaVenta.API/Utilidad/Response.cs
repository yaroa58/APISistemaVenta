namespace SistemaVenta.API.Utilidad
{ // Add T para recibir cualquier objeto
    public class Response<T>
    {
        // retornar informacion si la operacion ha sido exitosa
        public bool status { get;set; }
        public T value { get;set; }
        public string msg { get;set; }
    }
}
