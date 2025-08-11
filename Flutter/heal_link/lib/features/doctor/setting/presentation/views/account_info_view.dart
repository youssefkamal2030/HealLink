import 'package:flutter/material.dart';
import 'package:heal_link/core/widgets/custom_app_bar_pop_widget.dart';
import 'package:heal_link/features/doctor/setting/presentation/views/widgets/contact_info_section.dart';
import 'package:heal_link/generated/l10n.dart';

class AccountInfoView extends StatelessWidget {
  const AccountInfoView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16.0),
        child: Column(
          children: [
            CustomAppBarPopWidget(text: S.of(context).account),
            SizedBox(height: 24),
            ContactInfoSection(),
          ],
        ),
      ),
    );
  }
}
