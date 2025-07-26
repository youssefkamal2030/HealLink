import 'package:flutter/material.dart';
import 'package:flutter_localizations/flutter_localizations.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/utils/function/build_theme_data.dart';
import 'package:heal_link/core/utils/function/change_status_bar_color.dart';
// ignore: depend_on_referenced_packages
import 'package:intl/intl.dart';
import 'core/utils/constant.dart';
import 'generated/l10n.dart';

void main() {
  WidgetsFlutterBinding.ensureInitialized();
  changeStatusBarColor();
  runApp(const MyApp());
}

class MyApp extends StatelessWidget {
  const MyApp({super.key});

  @override
  Widget build(BuildContext context) {
    AppConstant.getSizes(context);
    return MaterialApp.router(
      theme: buildThemeData(),
      debugShowCheckedModeBanner: false,
      locale: const Locale('en'),
      localizationsDelegates: [
        S.delegate,
        GlobalMaterialLocalizations.delegate,
        GlobalWidgetsLocalizations.delegate,
        GlobalCupertinoLocalizations.delegate,
      ],
      supportedLocales: S.delegate.supportedLocales,
      routerConfig: AppRouter.router,
    );
  }
}

bool isArabic() {
  return Intl.getCurrentLocale() == 'ar';
}
