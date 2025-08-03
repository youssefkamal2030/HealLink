import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/core/widgets/custom_app_bar_pop_widget.dart';
import 'package:heal_link/features/doctor/setting/presentation/views/widgets/setting_field.dart';
import 'package:heal_link/generated/l10n.dart';

class SettingView extends StatelessWidget {
  const SettingView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16.0),
        child: Column(
          children: [
            CustomAppBarPopWidget(text: S.of(context).setting),
            SizedBox(height: 24),
            SettingField(
              onTap: () {
                context.push(AppRouter.accountInfoView);
              },
              label: S.of(context).account,
            ),
            SettingField(
              onTap: () {
                context.push(AppRouter.privacyView);
              },
              label: S.of(context).privacy_security,
            ),
            SettingField(label: S.of(context).data_storage),
            SettingField(label: S.of(context).notification_preferences),
            SettingField(label: S.of(context).language),
            SettingField(label: S.of(context).app_version_updates),
          ],
        ),
      ),
    );
  }
}
