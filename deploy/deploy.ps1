gci -Recurse ./volumes | % {kubectl apply -f $_.FullName}
gci -Recurse ./services | % {kubectl apply -f $_.FullName}
gci -Recurse ./app | % {kubectl apply -f $_.FullName}