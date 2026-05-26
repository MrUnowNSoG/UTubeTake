using System;
using System.Collections.Generic;
using System.Text;

namespace UTubeTake.Code.Tools {

    internal sealed class ErrorHandlService {

        public event Action? OnCatchError;


        public void CathcError(string exeption) {
            OnCatchError?.Invoke();
        }

    }
}
