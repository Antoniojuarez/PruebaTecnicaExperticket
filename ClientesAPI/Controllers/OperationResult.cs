namespace ClientesAPI.Controllers
{
    public class OperationResult<TEntity>
    {
        public bool Success => !MessageList.Any();
        public List<string> MessageList { get; private set; }
        public TEntity Response { get; set; }

        public OperationResult()
        {
            MessageList = new List<string>();
        }

        public void AddMessage(string message)
        {
            MessageList.Add(message);
        }

        public void SetSuccessResponse(TEntity entity)
        {
            Response = entity;
        }
    }
}
