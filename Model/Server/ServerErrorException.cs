using Model.Server.Entities;

namespace Model.Server;

public class ServerErrorException(Error error): Exception(error.ErrorMessage) {
}
