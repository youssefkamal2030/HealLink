import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_app_bar_pop_widget.dart';
import 'package:heal_link/features/doctor/personal_information/presentation/views/widgets/info_field.dart';
import 'package:heal_link/features/doctor/setting/presentation/views/widgets/setting_field.dart';
import 'package:heal_link/generated/l10n.dart';

class PrivacyView extends StatelessWidget {
  const PrivacyView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16.0),
        child: Column(
          children: [
            CustomAppBarPopWidget(text: S.of(context).privacy_security),
            SizedBox(height: 24),
            SettingField(label: S.of(context).biometric_authentication),
            InfoField(
              label: S.of(context).show_my_availability_status,
              isEditable: true,
            ),
            SizedBox(height: 8),
            InfoField(label: S.of(context).login_alerts, isEditable: true),
          ],
        ),
      ),
    );
  }
}
