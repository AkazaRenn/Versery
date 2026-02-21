using System;
using System.Collections.Generic;
using System.Text;

namespace View; 
internal class Messages {
    public record NavigationRequest(Type PageType, object? Parameter = null);
}
