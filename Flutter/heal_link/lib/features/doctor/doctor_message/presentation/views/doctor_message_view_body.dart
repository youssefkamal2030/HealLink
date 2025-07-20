import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/app_images.dart';
import 'package:heal_link/core/widgets/custom_app_bar.dart';
import 'package:heal_link/core/widgets/custom_text_form_field2.dart';
import 'package:heal_link/features/doctor/doctor_message/data/models/message_model.dart';
import 'package:heal_link/core/widgets/custom_tab_button.dart';
import 'package:heal_link/features/doctor/doctor_message/presentation/views/widgets/message_item.dart';
import 'package:heal_link/features/doctor/doctor_message/presentation/views/widgets/no_messages_widget.dart';
import 'package:heal_link/generated/l10n.dart';

class DoctorMessageViewBody extends StatefulWidget {
  const DoctorMessageViewBody({super.key});

  @override
  State<DoctorMessageViewBody> createState() => _DoctorMessageViewBodyState();
}

class _DoctorMessageViewBodyState extends State<DoctorMessageViewBody> {
  String selectedTab = "All";

  @override
  Widget build(BuildContext context) {
    List<MessageModel> filteredMessages =
        selectedTab == S.of(context).all
            ? allMessages
            : allMessages.where((msg) => !msg.isRead).toList();

    return Scaffold(
      appBar: CustomAppBar(
        title: S.of(context).messages,
        showBackButton: false,
      ),
      body: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 16.0),
        child: Column(
          children: [
            CustomTextFormField2(
              hintText: S.of(context).search_for_a_patient,
              keyboardType: TextInputType.text,
              controller: TextEditingController(),
              validator: (value) {
                return null;
              },
              prefixIcon: AppImages.search,
              borderRadiusSize: 8,
            ),
            const SizedBox(height: 15),
            Row(
              mainAxisAlignment: MainAxisAlignment.start,
              children: [
                CustomTabButton(
                  label: S.of(context).all,
                  isSelected: selectedTab == S.of(context).all,
                  onTap: () {
                    setState(() {
                      selectedTab = S.of(context).all;
                    });
                  },
                ),
                const SizedBox(width: 10),
                CustomTabButton(
                  label: S.of(context).unread,
                  isSelected: selectedTab == S.of(context).unread,
                  onTap: () {
                    setState(() {
                      selectedTab = S.of(context).unread;
                    });
                  },
                ),
              ],
            ),
            const SizedBox(height: 10),
            Expanded(
              child:
                  filteredMessages.isEmpty
                      ? const NoMessagesWidget()
                      : ListView.separated(
                        itemCount: filteredMessages.length,
                        separatorBuilder: (_, __) => const Divider(height: 15),
                        itemBuilder: (context, index) {
                          return MessageItem(message: filteredMessages[index]);
                        },
                      ),
            ),
          ],
        ),
      ),
    );
  }
}
